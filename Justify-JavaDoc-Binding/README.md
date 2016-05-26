[Xamarin JavaDoc](https://developer.xamarin.com/guides/android/advanced_topics/binding-a-java-library/customizing-bindings/naming-parameters-with-javadoc/)


You did not specify which warning the `JARTOXML` task of the `ExportJarToXml` Target is producing. If you do not know it:

    cd {YourJavaBindingLibraryDirectory}
    rm -Rf bin obj # Remove build artifacts so all tasks all executed
    xbuild /verbosity:debug # /verbosity:diagnostic if needed

*Note: Try using `verbosity:diagnostic` if you do not see any warning produced...*

Search the output for the `JARTOXML` task...

The number one issue that I see is:

###J2X8001

------

    JARTOXML:  warning J2X8001: Couldn't access javadocs at specified docpath.  Continuing without it...

------

*FYI: This example uses [bluejamesbond/TextJustify-Android][1], a fantastic Text justification library ;-)*


**First**, double check the relationship of `JavaDocPaths` in your `.csproj`:

`.csproj` contains:

    <PropertyGroup>
      <JavaDocPaths>JavaDocs/textjustify-android-2.1.6-javadoc</JavaDocPaths>
    </PropertyGroup>

Equals Project dir:

    TextJustifyAndroid.csproj
    ~~~~
    ├── JavaDocs
    │   ├── textjustify-android-2.1.6-javadoc
    ~~~~

Note: This is relative to the binding project `.csproj` location, **not the solution.**

**Next**, make your that you have expanded the JavaDoc jar and it is in its original structure. 

Here is how I personally structure the project tree:


    ├── Jars
    │   └── textjustify-android-2.1.6.aar
    ├── JavaDocs
    │   └── textjustify-android-2.1.6-javadoc.jar

I copy the *.javadoc.jar(s) in the my `JavaDocs` subdirectory, I then expand them (I use `unzip`, but any tool that preserves the dir structure is fine)

    unzip textjustify-android-2.1.6-javadoc.jar -d textjustify-android-2.1.6-javadoc

This results in:

    ├── Jars
    │   └── textjustify-android-2.1.6.aar
    ├── JavaDocs
    │   ├── textjustify-android-2.1.6-javadoc
    │   │   ├── META-INF
    │   │   │   └── MANIFEST.MF
    │   │   ├── allclasses-frame.html
    │   │   ├── allclasses-noframe.html
    │   │   ├── com
    │   │   │   └── bluejamesbond
    │   │   │       └── text
    │   │   ├── script.js
    │  ~~~~~~~~~~~~~~~~~~~~~~~~
    │   │   ├── serialized-form.html
    │   │   └── stylesheet.css
    │   └── textjustify-android-2.1.6-javadoc.jar

FYI: I preserve the version in the directory name to aid in versioning as I might be working with multiple versions of the same `.aar|.jar` within the same project... but you can rename it, just make sure it matches what is in your `.csproj`.

Running `xbuild /verbosity:debug` should result in the the `ExportJarToXml` target producing no warning:

    ~~~~
    Target ExportJarToXml:
    		Tool /usr/bin/java execution started with arguments: -XX:-UseSplitVerifier -jar /Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/mandroid/jar2xml.jar --jar=.../Justify/TextJustifyAndroid/obj/Debug/library_project_jars/classes.jar --ref=/Users/sushi/Library/Developer/Xamarin/android-sdk-macosx/platforms/android-22/android.jar --out=.../Justify/TextJustifyAndroid/obj/Debug/api.xml --javadocpath=.../Justify/TextJustifyAndroid/JavaDocs/textjustify-android-2.1.6-javadoc
    ~~~~

 


  [1]: https://github.com/bluejamesbond/TextJustify-Android