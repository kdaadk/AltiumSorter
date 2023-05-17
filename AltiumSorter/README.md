It's a custom sorter for large files with a specific lines.

To launch it write in console `.\AltiumSorter.exe 50 2000000`
1. the first argument is a file size chunk in mb, for example 50mb chunks used memory ~ 500mb
2. the second argument is a total lines that will generated file example, and 2kk lines ~ 1gb

At first program starts create file with random lines
At second program sorts thar file
Example output:

```
started generating file with totalLines: 2000000
ended generating in ms: 15110
started sorting file with fileChunkSize in mb: 50
ended sorting in ms: 28750
```