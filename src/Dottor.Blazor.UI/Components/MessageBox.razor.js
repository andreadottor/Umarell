export function init(element) {
    var myModal = new bootstrap.Modal(element);
    return myModal;
}

export function show(myModal) {
    myModal.show();
}

export function hide(myModal) {
    myModal.hide();
}
