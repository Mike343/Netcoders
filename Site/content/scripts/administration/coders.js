function menu_normal(item) {
	if (item.attr("class") == "last") {
		item.attr("class", "last");
	} else if (item.attr("class") == "last selected") {
		item.attr("class", "last");
	} else {
		item.attr("class", "normal");
	}
}

function menu_selected(item) {
	if (item.attr("class") == "last") {
		item.attr("class", "last selected");
	} else if (item.attr("class") == "last selected") {
		item.attr("class", "last selected");
	} else {
		item.attr("class", "selected");
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