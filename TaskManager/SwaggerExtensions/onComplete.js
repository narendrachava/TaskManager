$(function () {
    $('#input_apiKey').attr("placeholder", "Access Token");
    $('#input_apiKey').off();
    $('#input_apiKey').blur(function () {
        var token = this.value;
        token = 'Bearer ' + token;
        var apiKeyAuth = new window.SwaggerClient.ApiKeyAuthorization("Authorization", token, "header");
        window.swaggerUi.api.clientAuthorizations.add("token", apiKeyAuth);
    });
})();