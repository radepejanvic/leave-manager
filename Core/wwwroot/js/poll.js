$(document).ready(function () {
    $('#pollButton').click(function () {
        $.ajax({
            url: '/admin/home/pollEmails',
            type: 'POST',
            success: function () {
                window.location.reload(); 
            },
        });
    });
});