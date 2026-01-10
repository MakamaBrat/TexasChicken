using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/Game.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "TexasChicken.aab";
        string apkPath = "TexasChicken.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 = "MIIJ4gIBAzCCCYwGCSqGSIb3DQEHAaCCCX0Eggl5MIIJdTCCBawGCSqGSIb3DQEHAaCCBZ0EggWZMIIFlTCCBZEGCyqGSIb3DQEMCgECoIIFQDCCBTwwZgYJKoZIhvcNAQUNMFkwOAYJKoZIhvcNAQUMMCsEFHByoclMEYrn1n/BjtNpUTNt9/toAgInEAIBIDAMBggqhkiG9w0CCQUAMB0GCWCGSAFlAwQBKgQQQW8TsytVcBWsfKKKimciIwSCBNCs22CAg0kRiKx6IKrkpvMi6xE6J6CGli+tSa948rvbHWMA6WqWSm5xLdeDfj3wuStAUIn3nGUMk+yd6XGmLARyjoh3GK5WTz0rSWMEjHYxnuahJyaMhloV0DZAN187d4DuRib3jq993zu6OrXTgRA3FXu5H8BLpzJ9bvNxfKNQHgHW32TPZbQBwcVfIQIGHcY3+nvq+zKt2l0YeqNonaQYL2W5bLD0KDR1RYvz7DVNaAslg+di1mHrHqoM//PVxSuwNTXQ2hCgLAvpj+f+Hwh538kAkrhUUhcbyi+10ZzC9ovZjiPJ+4G4cuFp3LmKNTwwjWq1gWmcr8wdqiBsReRl5TvfCdmlhvLJKLUHUi3WWPYBeF4dSg1MPm7rcm6DVBSpLNc5Rl8o4xHn3PUYUtJrCq0MmmLP+0cUKyRCvvNIntscX+CkDvFVSgMxsD6JJdMPVrgr3dJCKtea6GCJYCrobhwufEflOT/KjsIEMVx8ldsCmpCmEQA+KHW2dJrdfVlsFkZl6hAgwPw0S06Utz/akcw69+R+BCLfYWKL3HlpumAfc7/qjrbDUKRpnGo2rrEfjHABO0GEjHlByyFYE8QE3s4Pw5wLNH7g77dSr8UpvoOI1Ot8dwxk2Xca1c8cjRjdR5gALTLy05uUO8cpR+HVpzsRUAYJUirW8qItmaR8UJJRsbum25BWP1tSDwu//hywP11ksk/ID//prMdCADGDrok+N+hnlkKnC5GCANDra//o8WjGE9kDXQZPs9xSuDKRA7SzU8UUjl5uj2NhZz6/vYo3gy0NvW+1dA56JtOvJAd4PKxmxHL3oRWX2aGMowErUSt9iXvSyvVWy+lxsAYTxp/mJjfUFywWL8dxQKZyNjhE9ze/zWBTgY6DXIRF64cMtAeCrMlZhSKgTq5pkqp3K0i8HaJvZF0gZuzkL4oFQhFV9UWGxLgTr+4r0t/rMjdpPD/ZooyE/dgQtI3Y+SepHCMRJE/cJesVUqWAB1Lu4zw1o68pHHQXZVKhIVKlGhjr4KFdB6j/FusQpuBYB4xzWv6AWhZme3d7q9IkR/d7h4sxREFWK3PgewIJA1CejBZyWbrdVBdQ0U4qf7mu68vHu5aL09W4zInuO+jCL3zz4UhQc3cU6V7qkHrEFv5We+XZogZ7h8Gh4CaThXl//tL6KI+m0jGYkLcK+AbAFkDFGQCraxvRiRIn2hU0MhUu1NsFOvZUvljKzf/9U/0WjY8iYv9zK/KfplKI+NIbg9Yplha8Xx5jGLqOVM2QmdW60p8leChDgDUAXL1fD9OHJgWyDWbjzpveIAVPcimIy+im8k/5WJePzGcIeRnjn/OzQCZ/PUs+RZh9tdU7hg0WDxB7grFZlkiBtDGgTgWjdUUg7M6/K2AiYyaaNviDjuGJIYx4ARjT0/ftpT38/nEmx/QiJ/poRruTJah0NwVTlgnNbyOhlfkxCfF4hem4mwI3KgoHLhpn/Qkgay9zkCvw4+L/+KsHkpoG0a33zGEdA34a8gIuxA3lu5slDBFD/Wuj38k1PdOCv2SeBPIWLr2IK/Sazao03gT72I+8Cfa7/9IDWOD7xxJOCMs2GR4gWiGjHxWpJgwE3EVO3iCJt+sg5XF+rMr8GaWnyEP16k8M7So84TE+MBkGCSqGSIb3DQEJFDEMHgoAdABlAHgAYQBzMCEGCSqGSIb3DQEJFTEUBBJUaW1lIDE3Njc5Nzc0NTU2NDkwggPBBgkqhkiG9w0BBwagggOyMIIDrgIBADCCA6cGCSqGSIb3DQEHATBmBgkqhkiG9w0BBQ0wWTA4BgkqhkiG9w0BBQwwKwQUTHgBYcunoJm7PeBayPlCAcnydmwCAicQAgEgMAwGCCqGSIb3DQIJBQAwHQYJYIZIAWUDBAEqBBAzYZd0nj7B/LbMO/MBfi3DgIIDMJEGgb2va+J6GrZRkJGzglt/6H8UAD4/YD/R4mwETEPON0Kkl5c/8KryW72lpHIbwLg3sqEA1O49CJhTvymwe4Fru3TYLmoU1noOjXrBg4pLiR0I/LEGV1pGPBGlQqJyPChF/1vfQ/JLcG/hGlZ/2Rxfzc01+oIagLil2NXK9ZILP8wVG863xLzX+/lbwhg9ub6aPobl3enx2INFo9zqLzm44SNxPj+HT/SQ2BcpDklomFpeec7QVcMUBb1T8zlurn1/BSuTrb7+lCBJLNxhXz0/UfiPXTqC/eLSkXUgmlrF1sxVRM6WBZ75atSjvmpg3lRz6ET+ajB3DuhOe2tf9OgIK1cdi6eTU3slT3oobWWQ7Jv7nLmbblt2bovwhhIicyV02yMqNz+yp/CEM1pjEMGE+qChCrgKNbvAMNrpE0kTqT86PRYpwhTQEYPcTkMIZazQyNxHRjl0oMq4lnSWmCe1fCogw9dm1TOlc7xRCOnsesuQ0Fverc9Q21uyuaKzRjKUpROZxvapsDVQrIomyzRLMJ5BnsuXtJDLhXNNITTcutaDzOVpNYs8OEZtic0mphJRfdUerDc9vXtPcsCpLqrEDh1FXyr35ITwObKWrqG1xA5hwDeCDwS6Ve4kIegYu0Q1l49UURsKpXM/j6JZWER80IcZmr0Z/OWIzcjTAFNGef/sjForoqYs6ufU2FRTFdn3JM/gkFlsja3IpsnI3CIaAixXqE41JUlresNRKEheSHleGDsiAW2RIQ6Yner4N/hsfdKicHKDGiOvDHt286HKzjXV7Zhm+KOZzRIpJTtNZQCJHFX3v99qrSu+j+dVIrVcXKhroJuUFP43jsRQxeiU5f1U/3vdIxeLSaqBVL7NVb5C3/10R+1AWEsyPG7AtV/clJmRpzpv6HTQwtkeCvi5lpKwMwd0ReHhk8uUg87KO+LfaMBV5kbvrAcK3cAl3HhRWqVR/ISqX63wRDSVD5XEGgnDdxFtJzhMJjhMzOr6v3XPUNU+kHIhr1l7N4OCPkAGL7NB+JSv0Lu9/+JKIqqSbkAlN6RiunG15q5P3AhQgIQW4Ix4LUDlElnS41Vc8jBNMDEwDQYJYIZIAWUDBAIBBQAEIFb/9N215ofsbJZl7gPkQ7YoKrWAXaZ6+dvkjM1JYf9rBBSNXgSKSutNG67ovjjKoccD7YFVngICJxA=";
        string keystorePass = "123texas";
        string keyAlias = "texas";
        string keyPass = "123texas";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}
