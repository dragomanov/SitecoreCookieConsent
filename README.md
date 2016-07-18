# A cookie consent module for Sitecore
## Description
The module allows you to present the visitors with a consent notice, informing them the site uses cookies and showing a link to a cookie policy page. All of that is customizable from within Sitecore, independently for each site/language that you have on the instance.

For the front-end, the module uses the cookie consent JS tool from [Silktide](https://silktide.com/tools/cookie-consent/).

## Installation
1. Get the code from your corresponding Sitecore version branch
2. Copy the Sitecore.Kernel.dll & Sitecore.Mvc.dll from your website's bin folder to the libs/ folder
3. Put in your local instance settings into the TDS project
4. Deploy the solution
5. Customize and enable the module from the /sitecore/system/Modules/Cookie Consent/Settings/website item
6. Publish and voila :)

## Customization
The module gets the information for the current site by looking for an item with the site name in /sitecore/system/Modules/Cookie Consent/Settings
In case the item doesn't exist for this site and language, it skips rendering the consent notice and lets Sitecore process the request normally.
For each site that is going to display the notice, you'll need to create a settings item with the name of the site (as set in the sitecore section of the configuration) and enable it.

**Example:** <br />
> Site name: _Awesome Website_ <br />
> Settings item: /sitecore/system/Modules/Cookie Consent/Settings/_Awesome Website_

## Performance
The module was developed with speed in mind, so it doesn't affect the performance of the instance.
Once the settings item for a specific site/language has been cached by Sitecore, the whole processor takes less than a millisecond!
