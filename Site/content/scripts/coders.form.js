/* this function styles inputs with the type file. It basically replaces browse or choose with a custom button */
(function ($) {
    $.fn.file = function (options) {
        var settings = {
            width: 250
        };

        if (options) {
            $.extend(settings, options);
        }

        this.each(function () {
            var self = this;

            var wrapper = $("<a>").attr("class", "ui-input-file");

            var filename = $('<input class="file">').addClass($(self).attr("class")).css({
                "display": "inline",
                "width": settings.width + "px"
            });

            $(self).before(filename);
            $(self).wrap(wrapper);

            $(self).css({
                "position": "relative",
                "height": settings.image_height + "px",
                "width": settings.width + "px",
                "display": "inline",
                "cursor": "pointer",
                "opacity": "0.0"
            });

            if ($.browser.mozilla) {
                if (/Win/.test(navigator.platform)) {
                    $(self).css("margin-left", "-142px");
                } else {
                    $(self).css("margin-left", "-168px");
                };
            } else {
                $(self).css("margin-left", settings.image_width - settings.width + "px");
            };

            $(self).bind("change", function () {
                filename.val($(self).val());
            });
        });

        return this;
    };
})(jQuery);

$(document).ready(function () {
    $("input.focus").focus(function () {
        if (this.value == this.defaultValue) {
            this.value = "";
        }
        else {
            this.select();
        }
    });

    $("input.focus").blur(function () {
        if ($.trim(this.value) == "") {
            this.value = (this.defaultValue ? this.defaultValue : "");
        }
    });

    /* date picker */
    $(".date").datepicker({
        showOn: 'both',
        buttonText: ''
    });

    /* file input styling */
    $("input[type=file]").file({
        image_height: 28,
        image_width: 28,
        width: 240
    });

    /* button styling */
    $("input:submit, input:reset, button").button();
});