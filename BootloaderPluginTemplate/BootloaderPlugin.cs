using System.IO;
using System.Reflection;
using RegawMOD.Android;

namespace BootloaderPluginTemplate
{
    public partial class BootloaderPlugin : IBootloaderPlugin
    {
        /*
         * RegawMOD Bootloader Customizer Plugin Template ReadMe
         * 
         * 1.  Make sure the external reference to 'RegawMOD Bootloader Customizer.exe' is correct and valid.
         * 2.  In the Project Properties window, change the following to your own values:
         *      - Assembly Name
         *      - Default Namespace
         *      - Information in 'Assembly Information...'
         * 3.  Change the namespace of this class to the same as your Default Namespace you just defined
         * 4.  Drag and Drop your hboot.img (rename your image to hboot.img) and android-info.txt to the root of the project
         *     in the Solution Explorer, overwriting the placeholder hboot.img and android-info.txt files.
         * 5.  Make sure they are set to "Embedded Resource" in their properties window.
         * 6.  Edit ONLY the private fields below to match your configurations
         * 7.  Build and deploy your dll under the "Release" configuration
         */

        // The values listed below are just samples taken from the Evo 4G LTE plugin I wrote

        /// <summary>
        /// Your developer name
        /// </summary>
        private string developerName = "regaw_leinad";

        /// <summary>
        /// Your xda profile id. ie: in forum.xda-developers.com/member.php?u=2326081, 2326081 is the ID
        /// </summary>
        private int developerXdaProfileId = 2326081;

        /// <summary>
        /// Your personal website to be displayed on the plugin info page.  Leave as "" if you don't want to share
        /// </summary>
        private string developerPersonalWebsite = "http://memes.regawmod.com";

        /// <summary>
        /// A small description of your plugin, no limit on length, will be displayed in a scrollable text box.
        /// </summary>
        private string pluginDescription = "This plugin is for the HTC EVO 4G LTE on Sprint.\r\nThis modifies the LazyPanda 1.12.2222 hboot image.";

        /// <summary>
        /// The HTC Device this plugin is built for
        /// </summary>
        private HTC_DEVICE devicePlugin = HTC_DEVICE.JEWEL;

        /// <summary>
        /// The full version number of the hboot image you are editing
        /// </summary>
        private string originalHbootVersionNumber = "1.12.0000";

        /// <summary>
        /// The naming scheme of the firmware zips flashed in hboot.  Notice how the '.zip' is left off.
        /// </summary>
        private string firmwareZipName = "PJ75IMG";

        /// <summary>
        /// Set to true if you want to allow the Bootloader Banner Text to be changed
        /// </summary>
        private bool allowChangeBannerText = true;

        /// <summary>
        /// Offset (dec or hex) of Banner Text start (Location of ***UNLOCKED***)
        /// </summary>
        private long offsetBannerTextStart = 786060L;

        /// <summary>
        /// Max Length of Banner Text allowed (from 0x00 to the next 0x00)
        /// </summary>
        private int maxLengthBannerText = 16;

        /// <summary>
        /// Set to true if you want to allow the S-OFF Text to be changed to S-ON
        /// </summary>
        private bool allowChangeSOffText = true;

        /// <summary>
        /// Offset (dec or hex) of S-OFF Text start
        /// </summary>
        private long offsetSOffTextStart = 834585L;

        /// <summary>
        /// Set to true if you want to allow the Hboot Minor Version Text to be changed (ie: 1.12.0000, would be the 0000)
        /// </summary>
        private bool allowChangeMinorVersionText = true;

        /// <summary>
        /// Offset (dec or hex) of Hboot Minor Version Text start
        /// </summary>
        private long offsetMinorVersionTextStart = 49L;

        /// <summary>
        /// Set to true if you want to allow the HTC Development Disclaimer Splash Screen Text to be changed
        /// </summary>
        private bool allowChangeHtcDevSplashText = true;

        /// <summary>
        /// Offsets (dec or hex) of HTC Development Disclaimer Splash Screen Text start.  One Offset for each line (from 0x00 to the next 0x00)
        /// </summary>
        private long[] offsetsHtcDevSplashTextStart = { 784600L, 784620L, 784648L, 784684L, 784720L, 784744L };

        /// <summary>
        /// Corresponding Max Lengths of HTC Development Disclaimer Splash Screen Text allowed per line;
        /// </summary>
        private int[] maxLengthsHtcDevSplashText = { 17, 25, 32, 33, 21, 21 };

        /// <summary>
        /// Set to true if you want to allow an updated build timestamp on customization
        /// </summary>
        private bool allowChangeBuildDateTime = true;

        /// <summary>
        /// Offset of Build Date/Time Text Start
        /// </summary>
        private long offsetDateTimeTextStart = 786212L;

        /// <summary>
        /// Format of Date Time Text to be passed to DateTime.Now.ToString();  Most likely the default below
        /// </summary>
        private string formatDateTimeDisplay = "MMM dd yyyy,HH:mm:ss";

        /// <summary>
        /// Just in case there is an error getting the ToString of DateTime.Now, only this many chars will be written to the hboot image
        /// </summary>
        private int maxLengthDateTime = 20;

    /*
     * DO NOT MODIFY BELOW THIS LINE!
     * 
     * THIS IS THE LAYER BETWEEN YOUR PLUGIN AND 
     * REGAWMOD BOOTLOADER CUSTOMIZER
     * 
     * ANY CHANGES BELOW THIS LINE WILL RESULT IN A NON-FUNCTIONAL PLUGIN!
     * 
     */
        private Assembly assembly = Assembly.GetExecutingAssembly();
        public string DEVELOPER_NAME { get { return this.developerName; } }
        public int DEVELOPER_XDA_PROFILE_ID { get { return this.developerXdaProfileId; } }
        public string DEVELOPER_PERSONAL_WEBSITE { get { return this.developerPersonalWebsite; } }
        public string PLUGIN_DESCRIPTION { get { return this.pluginDescription; } }
        public HTC_DEVICE DEVICE { get { return this.devicePlugin; } }
        public Stream HBOOT_IMAGE { get { return assembly.GetManifestResourceStream(assembly.GetType(this.ToString()).Namespace + ".hboot.img"); } }
        public Stream ANDROID_INFO { get { return assembly.GetManifestResourceStream(assembly.GetType(this.ToString()).Namespace + ".android-info.txt"); } }
        public string BOOTLOADER_ORIG_VERSION { get { return this.originalHbootVersionNumber; } }
        public string FIRMWARE_ZIP_NAME { get { return this.firmwareZipName; } }
        public bool ALLOW_CHANGE_BANNER_TEXT { get { return this.allowChangeBannerText; } }
        public long OFFSET_BANNER_TEXT_START { get { return this.offsetBannerTextStart; } }
        public int LENGTH_MAX_BANNER_TEXT { get { return this.maxLengthBannerText; } }
        public bool ALLOW_CHANGE_S_OFF_TEXT { get { return this.allowChangeSOffText; } }
        public long OFFSET_S_OFF_TEXT_START { get { return this.offsetSOffTextStart; } }
        public bool ALLOW_CHANGE_MINOR_VERSION_NUMBER_TEXT { get { return this.allowChangeMinorVersionText; } }
        public long OFFSET_MINOR_VERSION_TEXT_START { get { return this.offsetMinorVersionTextStart; } }
        public bool ALLOW_CHANGE_HTC_DEVELOPMENT_DISCLAIMER { get { return this.allowChangeHtcDevSplashText; } }
        public long[] OFFSETS_HTC_DEVELOPMENT_DISCLAIMER { get { return this.offsetsHtcDevSplashTextStart; } }
        public int[] LENGTHS_MAX_HTC_DEVELOPMENT_DISCLAIMER { get { return this.maxLengthsHtcDevSplashText; } }
        public bool ALLOW_CHANGE_BUILD_DATE_TIME { get { return this.allowChangeBuildDateTime; } }
        public long OFFSET_DATE_TIME_TEXT_START { get { return this.offsetDateTimeTextStart; } }
        public string FORMAT_DATE_TIME_DISPLAY { get { return this.formatDateTimeDisplay; } }
        public int LENGTH_MAX_DATE_TIME { get { return this.maxLengthDateTime; } }
    }
}