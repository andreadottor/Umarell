export function appInit() {
    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
            document.body.classList.toggle('sb-sidenav-toggled');
        }

        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }
}

export function loadJs(sourceUrl) {
	if (sourceUrl.Length == 0) {
		console.error("Invalid source URL");
		return;
	}

	var tag = document.createElement('script');
	tag.src = sourceUrl;
	tag.type = "text/javascript";

	tag.onload = function () {
		console.log("Script loaded successfully");
	}

	tag.onerror = function () {
		console.error("Failed to load script");
	}

	document.body.appendChild(tag);
}