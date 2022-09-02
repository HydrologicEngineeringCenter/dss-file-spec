The purpose of this document is to describe the DSS file format. DSS files are used to store time series and other water resources related data types.  The binary DSS file is implemented with 64 bit (8 byte) words.  Data is stored in DSS using a key called a [pathname](https://www.hec.usace.army.mil/confluence/dssvuedocs/latest/introduction/general-concepts-for-hec-dss)

A dss file begins with a File [Header](file-header.md).  
<pre>
┌────────────────────────────────────────┐
│ZDSS,Header Size,Version, ...           │
│                                        │
│size of hash table (e.g. 8192)          |
│    ...                                 │
│Address of hash table (e.g. 178)        │
│                                        │
│                                        │
│                                        │
│ -97531 (end of Header Flag)            │
└────────────────────────────────────────┘
</pre>

The Header is followed by the file hash array (hash table).  The hash of a pathname is the index into the hash array, and the value at that index is the file address to a pathname-bin for that pathname.
<br>
example: [0, 0, 0, 0, ... ,11172,[5276](pathname-bins.md),0,0, 10972,...  ]

The [pathname bin](pathname-bins.md) contains meta-data for each pathname that hashes/point to that pathanme bin.  The meta-data for each bin item includes a pathname hash, status, record type, and file address to the record information.


The record information is 


