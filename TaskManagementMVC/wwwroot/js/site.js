// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Show overlay and loader
    function showLoader() {
        $('#overlay').fadeIn();
    }

    // Hide overlay and loader
    function hideLoader() {
        $('#overlay').fadeOut();
    }

    // Call showLoader when AJAX starts
    $(document).ajaxStart(function () {
        showLoader();
    });

    // Call hideLoader when AJAX completes
    $(document).ajaxStop(function () {
        hideLoader();
    });

    //Call hideLoader when AJAX fails
    $(document).ajaxError(function () {
        hideLoader();
    });
    // Call hideLoader when AJAX completes
    $(document).ajaxComplete(function () {
        hideLoader();
    });
    // Call hideLoader when AJAX Success
    $(document).ajaxSuccess(function () {
        hideLoader();
    });
});