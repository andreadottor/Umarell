export function initMap(cotainer, callback) {
    var marker;
    // inizializzazione mappa
    //
    var map = new google.maps.Map(cotainer, {
        zoom: 18,
        center: new google.maps.LatLng(45.8851534, 12.3373920),
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        disableDefaultUI: true,
        zoomControl: true
    });

    // funzione di creazione/posizionamento marker
    //
    var placeMarker = function (location) {
        if (marker) {
            marker.setPosition(location);
        } else {
            marker = new google.maps.Marker({
                position: location,
                map: map
            });
        }
        // notifico a Blazor le coordinate selezionate
        //
        callback.invokeMethodAsync("SetMarker", location.lat(), location.lng());
    }
    // rimango in ascolto del click sulla mappa per recuperare le coordinate
    //
    google.maps.event.addListener(map, 'click', function (event) {
        placeMarker(event.latLng);
    });


    return map;
}