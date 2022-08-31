pathname bins have essential path information, and a pointer to the record information for that pathname.

A block of two pathname bins is demonstrated below. Both pathnames below have the same file hash (5276). So scanning the list of pathname bins is required to find the exact info address that matches the path of interest.

 The file hash is a first level, and smaller, hash of the path, while the pathname hash.




| Example Address| Description | example |
| --- | ----------- | --|
|5276|Pathname hash|-7400211049837230000|
|5277|Status|1|
|5278|Pathname length|46|
|5278|Pathname size (int 8)|6|
|5279|Info address|[8477](record-info.md)|
|5280|Data type|100|
|5280|Catalog sort sequence|0|
|5281|Last write time|(1545211451036)   Dec 19, 2018, 09:24:11.036|
|5282|Date first value|(33715)   Apr 22, 1992|
|5282|Date Last value|(33723)   Apr 30, 1992|
|5283-5288|Pathname|/GREEN RIVER/GLENFIR/FLOW/01Apr1992/1Hour/OBS/  |
|||
|||
|5289|pathname hash|-5770584324254400000|
|5290|Status|1|
|5291|Pathname length|43|
|5291|Pathname size (int 8)|6|
|5292|Info address|66686|
|5293|Data type|100|
|5293|Catalog sort sequence|0|
|5294|Last write time|(1545211451160)   Dec 19, 2018, 09:24:11.160|
|5295|Date first value|(-4382)   Jan 1, 1888|
|5295|Date Last value|(-4017)   Dec 31, 1888|
|5296|Pathname|//SACRAMENTO/PRECIP-INC/01Jan1888/1Day/OBS/     |
|5302|pathname hash|0|
|+1| address to next block of pathname bins|5555|
