$(document).ready(function () {
	$("tr.compare").hide();

	$("a.compare").live("click", function (event) {
		var anchor = $(this);

		$("tr.compare").hide();

		$(anchor.attr("href")).show();

		return false;
	});
});