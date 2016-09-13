## Build status
| Windows | Linux   |
|-------- | ------- |
|[![Build status](https://ci.appveyor.com/api/projects/status/tqvoos5455vc1yvb/branch/master?svg=true)](https://ci.appveyor.com/project/Nexus87/pokeclone/branch/master)|[![Build Status](https://travis-ci.org/Nexus87/PokeClone.svg?branch=master)](https://travis-ci.org/Nexus87/PokeClone)|

## Building
To create the Solution and Project files use
```
Protobuild.exe --generate
```
For Linux:
```
mono ./Protobuild.exe --generate
```
After restoring NuGet packages the above command needs to be repeated