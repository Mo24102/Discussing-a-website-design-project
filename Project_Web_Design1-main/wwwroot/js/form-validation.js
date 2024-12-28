$(document).ready(function () {
    $("form").submit(function (event) {
        var isValid = true;

        var age = $("#Age").val();
        if (age <= 0) {
            isValid = false;
            alert("Age must be greater than 0.");
        }

        var fees = $("#Fees").val();
        if (fees <= 0) {
            isValid = false;
            alert("Fees must be greater than 0.");
        }

        if (!isValid) {
            event.preventDefault();
        }
    });
});
