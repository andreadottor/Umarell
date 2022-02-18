export function initMap(container, items, callback) {
    var markers = [];

    // inizializzazione mappa
    //
    var map = new google.maps.Map(container, {
        zoom: 18,
        center: new google.maps.LatLng(45.8851534, 12.3373920),
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        disableDefaultUI: false,
        zoomControl: true
    });

    var styleArray = [
        {
            featureType: "poi.business",
            stylers: [
                { visibility: "off" }
            ]
        }
    ];
    map.setOptions({ styles: styleArray });

    const infoWindow = new google.maps.InfoWindow();
    var bounds = new google.maps.LatLngBounds();
    items.forEach(item => {
        // creazione marker
        //
        var location = new google.maps.LatLng(item.latitude, item.longitude);
        let marker = new google.maps.Marker({
            position: location,
            map: map,
            icon: "/images/umarell_marker.png"
        });
        markers.push(marker);
        bounds.extend(location);
        // gestione tooltip con le informazioni del cantiere
        //
        google.maps.event.addListener(marker, "click", (function (id, marker, info) {
            return function () {
                info.close();
                // chiamo il metodo Blazor che si occupa di valorizzare il tooltip
                // con le info del cantiere selezionato
                //
                callback.invokeMethodAsync("SetMarkerInfo", id)
                    .then(markerInfoElement => {
                        // clono l'elemento html per non modificare/alterare quello originale
                        //
                        const clonedInfo = markerInfoElement.cloneNode(true);
                        // lo rendo visibile
                        //
                        clonedInfo.setAttribute("style", "display: block;");
                        info.setContent(clonedInfo);
                        info.open(map, marker);
                        // gestione chiusura tooltip
                        //
                        google.maps.event.addListener(map, 'click', function () {
                            info.close();
                        });
                    });
            }
        })(item.id, marker, infoWindow));

    })

    // Center/Set Zoom of Map to cover all visible Markers
    // https://stackoverflow.com/a/19304625/16405773
    map.fitBounds(bounds);

    return map;
}
