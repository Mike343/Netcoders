$(document).ready(function () {
	$(".messages .dismiss a").live("click", function (event) {
		var value = $(this).attr("href");
		var id = value.substring(value.indexOf('#') + 1);

		$("#" + id).fadeOut('slow', function () { });

		return false;
	});
});