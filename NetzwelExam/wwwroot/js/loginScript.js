function loginUser() {
    alert($("#loginForm").serialize());
    $.ajax({
        url: "Login/Index",
        data: $("#loginForm").serialize(),
        type: "POST",
        cache: false,
        async: true,
        contentType: "application/json",
        success: function () {
            alert("all good");
        },
        error: function () {
            alert(":(");
        }
    });
}