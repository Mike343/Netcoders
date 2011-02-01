$(document).ready(function () {
	var h1 = $("div.content").height();
	var h2 = $("div.filter").height();

	if (h1 < h2) {
		$("div.content").height(h2);
	}
});