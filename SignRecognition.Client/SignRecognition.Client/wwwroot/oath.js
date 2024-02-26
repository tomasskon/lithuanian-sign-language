let authenticationStateProviderInstance = null;

function initializeGoogleOath(clientId, blazorSchoolAuthenticationStateProvider)
{
    document.cookie = `g_state=;path=/;expires=Thu, 01 Jan 1970 00:00:01 GMT`;
    authenticationStateProviderInstance = blazorSchoolAuthenticationStateProvider;
    google.accounts.id.initialize({ client_id: clientId, use_fedcm_for_prompt: true, callback: successCallback });
}
   
function googleOathPrompt()
{
    google.accounts.id.prompt();
}

function successCallback(googleResponse)
{
    authenticationStateProviderInstance.invokeMethodAsync("GoogleLogin", { ClientId: googleResponse.clientId, SelectedBy: googleResponse.select_by, Credential: googleResponse.credential });
}