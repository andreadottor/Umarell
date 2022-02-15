export function initMap(cotainer, items, callback) {
    var markers = [];

    // inizializzazione mappa
    //
    var map = new google.maps.Map(cotainer, {
        zoom: 18,
        center: new google.maps.LatLng(45.8851534, 12.3373920),
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        disableDefaultUI: true,
        zoomControl: true
    });

    items.forEach(item => {
        // creazione marker
        //
        var location = new google.maps.LatLng(item.latitude, item.longitude);
        let marker = new google.maps.Marker({
            position: location,
            map: map
        });
        markers.push(marker);

        // gestione tooltip con le informazioni del cantiere
        //
        google.maps.event.addListener(marker, "click", (function (id, marker) {
            return function () {
                const infoWindow = new google.maps.InfoWindow();
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
                        infoWindow.setContent(clonedInfo);
                        infoWindow.open(map, marker);
                        // gestione chiusura tooltip
                        //
                        google.maps.event.addListener(map, 'click', function () {
                            infoWindow.close();
                        });
                    });
            }
        })(item.id, marker));

    })

    return map;
}
