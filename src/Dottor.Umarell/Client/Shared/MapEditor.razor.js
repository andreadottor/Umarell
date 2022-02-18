﻿export function initMap(container, callback) {
    var marker;
    // inizializzazione mappa
    //
    var map = new google.maps.Map(container, {
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
        // TODO: trovare soluzione migliore
        //
        map._marker = marker;
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

// Clear marker from Blazor
//
export function clearMarker(map) {
    var marker = map._marker;
    if(marker)
        marker.setMap(null);
}

// Change/set marker position from Blazor
//
export function setMarker(map, position) {
    var marker = map._marker;
    var location = new google.maps.LatLng(position.latitude, position.longitude);

    if (marker) {
        marker.setPosition(location);
    } else {
        marker = new google.maps.Marker({
            position: location,
            map: map
        });
    }
}