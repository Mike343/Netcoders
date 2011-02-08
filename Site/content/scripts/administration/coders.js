function menu_normal(item) {
	var options = [];

	options["first"] = "first";
	options["first selected"] = "first";
	options["last"] = "last";
	options["last selected"] = "last";
	options["normal"] = "normal";
	options["normal selected"] = "normal";
	options["selected"] = "normal";

	if (item.attr("class") == "") {
		item.attr("class", "normal");
	} else {
		item.attr("class", options[item.attr("class")]);
	}
}

function menu_selected(item) {
	var options = [];

	options["first"] = "first selected";
	options["first selected"] = "first selected";
	options["normal"] = "normal selected";
	options["normal selected"] = "normal selected";
	options["last"] = "last selected";
	options["last selected"] = "last selected";

	if (item.attr("class") == "") {
		item.attr("class", "selected");
	} else {
		item.attr("class", options[item.attr("class")]);
	}
}

$(document).ready(function () {
	$("#menu ul ul").css({ display: "none" });

	$("#menu ul li").hover(
		function () {
			$(this).find('ul:first').css({ visibility: "visible", display: "none" }).show(400);
		},
		function () {
			$(this).find('ul:first').css({ visibility: "hidden" });
		}
	);

	$("#menu ul li").hover(
        function () {
        	menu_selected($(this));
        	$(this).find('ul:first').css({ visibility: "visible", display: "none" }).show(400);
        }, 
		function () {
        	menu_normal($(this));
        	$(this).find('ul:first').css({ visibility: "hidden" });
        }
    );

	$("#menu ul ul").hover(
        function () {
        	menu_selected($(this).parent("li"));
        },
        function () {
        	menu_normal($(this).parent("li"));
        }
    );

	$(".messages .dismiss a").live("click", function (event) {
		var value = $(this).attr("href");
		var id = value.substring(value.indexOf('#') + 1);

		$("#" + id).fadeOut('slow', function () { });

		return false;
	});
});