const apiUrl = "/api/images",
    metadataFirstItemsNumber = 6;

const vm = new Vue({
    el: "#app",
    data: {
        editMode: false,
        fileInput: null,
        imageInfo: null,
        imageInfoList: [],
        map: null
    },
    computed: {
        hasMetadata() {
            return this.imageInfo && Object.keys(this.imageInfo.Metadata).length > 0;
        },
        coordinatesAvailable() {
            return this.hasMetadata && this.imageInfo.Metadata["GPS Latitude"] &&
                this.imageInfo.Metadata["GPS Longitude"] || false;
        },
        metadataFirstItems() {
            return this.getMetadata(0, metadataFirstItemsNumber);
        },
        metadataRestItems() {
            return this.getMetadata(metadataFirstItemsNumber);
        },
        hasMetadataRestItems() {
            return Object.keys(this.metadataRestItems).length > 0;
        },
        replacementText() {
            return this.getReplacementText(this.imageInfo ? this.imageInfo.Description : null);
        }
    },
    methods: {
        convertCoordinate(coordinate) {
            const [deg, min, sec] = coordinate.replace(",", ".").split(" ")
                .map(value => parseFloat(value.replace(/[^\d.]/g, "")));

            return deg + min / 60 + sec / 3600;
        },
        getMetadata(from, to) {
            const metadata = { };

            if (!this.imageInfo) {
                return metadata;
            }

            const propNames = Object.keys(this.imageInfo.Metadata).slice(from, to);

            if (propNames.length === 0) {
                return metadata;
            }

            propNames.forEach(propName => metadata[propName] = this.imageInfo.Metadata[propName]);

            return metadata;
        },
        getReplacementText(text) {
            if (!text) {
                return "No replacement text provided";
            }

            let replacementText = text.substr(0, 30);

            if (replacementText.length < text.length) {
                replacementText += "...";
            }

            return replacementText;
        },
        showMap() {
            const coordinates = [
                this.convertCoordinate(this.imageInfo.Metadata["GPS Latitude"]),
                this.convertCoordinate(this.imageInfo.Metadata["GPS Longitude"])
            ];

            if (this.map) {
                this.map.setCenter(coordinates, 10);
            } else {
                this.map = new ymaps.Map("map", {
                    center: coordinates,
                    zoom: 10
                });
            }

            const placemark = new ymaps.Placemark(coordinates);

            this.map.geoObjects.removeAll();
            this.map.geoObjects.add(placemark);
        },

        fetchThumbnails() {
            fetch(apiUrl)
                .then(response => response.json())
                .then(data => this.imageInfoList = data);
        },

        handleEditSaveActionClick(event) {
            event.preventDefault();

            if (!this.editMode) {
                this.editMode = true;

                return;
            }

            fetch(`${apiUrl}/${this.imageInfo.Id}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ description: this.imageInfo.Description })
            }).then(response => {
                if (response.ok) {
                    this.editMode = false;
                }
            });
        },
        handleThumbnailClick(imageInfoId) {
            fetch(`${apiUrl}/${imageInfoId}`)
                .then(response => response.json())
                .then(data => {
                    this.editMode = false;
                    this.imageInfo = data;

                    if (this.coordinatesAvailable) {
                        this.showMap();
                    }
                });
        },
        handleUploadActionClick() {
            const files = this.fileInput.files;

            if (files.length === 0) return;

            const formData = new FormData();

            for (let i = 0; i < files.length; i++) {
                formData.append("file" + (i + 1), files[i]);
            }

            fetch(apiUrl, {
                method: "POST",
                body: formData
            }).then(response => {
                if (response.ok) {
                    this.fetchThumbnails();
                }
            });
        }
    },

    mounted() {
        this.fileInput = document.getElementById("file-input");

        this.fetchThumbnails();
    },
    beforeDestroy() {
        if (this.map) {
            this.map.destroy();
        }
    }
});
