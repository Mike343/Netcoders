var classes = [];

classes["role"] = "role role-selected";
classes["role role-administrator"] = "role role-administrator role-administrator-selected";
classes["role role-selected"] = "role";
classes["role role-administrator role-administrator-selected"] = "role role-administrator";
classes["checkbox"] = "checkbox checkbox-selected";
classes["checkbox checkbox-administrator"] = "checkbox checkbox-administrator checkbox-administrator-selected";
classes["checkbox checkbox-selected"] = "checkbox";
classes["checkbox checkbox-administrator checkbox-administrator-selected"] = "checkbox checkbox-administrator";

function set_table_head_class(element) {
	element.attr("class", classes[element.attr("class")]);
}

function set_checkbox_class(element) {
	element.attr("class", classes[element.attr("class")]);
}

$(document).ready(function () {
	$("tbody[id*=checkboxes_]").each(function () {
		$(this).hide();
	});

	$(".expander").each(function () {
		$(this).attr("class", "expander plus");
	});

	$(".privileges-group").live('click', function (event) {
		var element = $(this);
		var id = element.attr("title");
		var checked = element.attr('checked');

		set_table_head_class($("#" + id));

		if (checked) {
			$("#checkboxes_" + id).show();
			$("#expander_" + id).attr("class", "expander minus");
		} else {
			$("#checkboxes_" + id).hide();
			$("#expander_" + id).attr("class", "expander plus");
		}

		$("." + id).each(function () {
			this.checked = checked;
			set_checkbox_class($(this).parent().parent());
		});
	});

	$(".checkboxes input[type='checkbox']").live('click', function (event) {
		set_checkbox_class($(this).parent().parent());
	});

	$(".expander").live('click', function (event) {
		var value = $(this).attr("href");
		var identifier = value.substring(value.indexOf('#') + 1);
		var checkboxes = $("#checkboxes_" + value.substring(value.indexOf('#') + 1));
		var image = $("#expander_" + identifier);

		if (checkboxes.is(":hidden")) {
			checkboxes.show();
			image.attr("class", "expander minus");
		} else {
			checkboxes.hide();
			image.attr("class", "expander plus");
		}

		return false;
	});
});