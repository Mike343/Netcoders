$(document).ready(function () {
	$("select.reputation").selectmenu({
		style: 'dropdown',
		width: 100,
		maxHeight: 200
	});

	var h2 = $("div.searches").height($("div.content").height() + 28);
});