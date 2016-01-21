#Semantic Versioning
A Semantic Versioning implementation in .NET based on [SemVer 2.0](http://semver.org), but more strictly.

The specification as follows:

1. The version number pattern: 
```
<Major>.<Minor>.<Patch>[-PreReleaseStage[.PreReleaseNumber]][+Build]
```
2. The Major, Minor,Patch and Build versions are compatible with SemVer 2.0;
3. Ther pre-release version is optional and be composed of PreReleaseStage and PreReleaseNumber segments; 
4. The PreReleaseStage allowed values (***case-insensitive***): [ALPHA, BETA, RC]; 
5. The PreReleaseNumber is same as Patch version, and optionally.

Examples:

* 0.2.1
* 0.2.1-Beta.5
* 0.2.1-ALPAH.50+1233