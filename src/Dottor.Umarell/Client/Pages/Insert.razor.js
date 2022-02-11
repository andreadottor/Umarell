export function initMap(cotainer, callback) {
    var map = new google.maps.Map(cotainer, {
        zoom: 18,
        center: new google.maps.LatLng(45.8851534, 12.3373920),
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        disableDefaultUI: true,
        zoomControl: true
    });
    var marker;
    var placeMarker = function (location) {
        if (marker) {
            marker.setPosition(location);
        } else {
            marker = new google.maps.Marker({
                position: location,
                map: map
            });
        }

        callback.invokeMethodAsync("SetMarker", location.lat(), location.lng());
    }

    google.maps.event.addListener(map, 'click', function (event) {
        placeMarker(event.latLng);
    });


    return map;
}