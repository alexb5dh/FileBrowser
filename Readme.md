# Table of Contents
1. [Routing](#routing)
2. [Statistics](#statistics)
3. [API](#api)

![image](https://cloud.githubusercontent.com/assets/4650832/18976754/f58c86ee-86bc-11e6-895a-5262ef8c2ad2.png)

# Routing
![image](https://cloud.githubusercontent.com/assets/4650832/18976403/46d7c52a-86ba-11e6-850b-123fb1031ba2.png)  
Site uses AngularJS routing for file system browsing.  
It allows easily integrate directory path with browser history functionality (back, forward, reload) and to keep track of current location in address bar.  
"Hashbang" is used to handle full Windows pathes with drive names.

Directory content is preloaded in route resolving stage to make sure directory is accessible and prevent navigation in case of failure.

# Statistics
Statistics (file-by-size-range counting) takes a noticable amount of time and disk throughput for large folders with tens of thousands of files. Application tries to minimize this by cancelling unnecessary calculation in case of route change.

Calculation ignore folders inaccessible due to insufficient rights and files/folders with full name exceeding 255 character limit.

# API
## Browse
`GET /browse/?path=<path>`  
Returns list of files and folders in the directory specified by `path`.

```json
{
    "folders": {
        "name": "",
        "path": ""
    },
    "files": {
        "name": "",
        "path": ""
    }
}
```

## Download
`GET /download/?path=<path>`  
Downloads the file specified by `path`.  

## Statistics
`GET /statistics/?path=<path>`  
Recursively calculates files by size ranges in directory specified by `path`.

```json
{
    "<= 10MB": 0,
    "10MB - 50MB": 0,
    ">= 100MB": 0
}
```
