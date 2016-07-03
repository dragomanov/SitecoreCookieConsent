<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScriptJs.ascx.cs" Inherits="Sitecore.CookieConsent.layouts.Modules.CookieConsent.ScriptJs" %>

<!-- Begin Cookie Consent plugin by Silktide - http://silktide.com/cookieconsent -->
<script type="text/javascript">
    window.cookieconsent_options = {
        "message": "<%= Model.Message %>",
        "dismiss": "<%= Model.DismissButton %>",
        "learnMore": "<%= Model.LearnMore %>",
        "link": "<%= Model.PolicyLink %>",
        "theme": "<%= Model.Theme %>"
    };
</script>

<script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/cookieconsent2/1.0.10/cookieconsent.min.js"></script>
<!-- End Cookie Consent plugin -->
