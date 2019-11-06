# Selector

Selector is a simple data filter which takes a file with timestamped data and splits it into chunks based on the selected timestamp range(s).
It can extract a single range from the file, or multiple when a configuration file with ranges is specified. 

## Usage

To split any data recorded with UXC, launch the program with the following command line arguments. Note, the character `^` only breaks up a long Windows command line for readability:

```
Selector.exe ET_data.json ^
--format JSON ^
--timestamp-field Timestamp ^
--timestamp-format date ^
--reference-timestamp-field TrackerTicks ^
--reference-timestamp-format ticks:us
--log-format CSV ^
--log ET_data.log.csv ^
--output ET_data.fixed.json ^
```

Description of used parameters:
* `ET_data.json` - path to the input file with array of UXC GazeData objects; if omitted, the standard input stream is used.
* `--format` - format of the input data, can be omitted if the file with `.json` or `.csv` extension is used; it is required when data is read from the standard input stream.
* `--timestamp-field` - name of the attribute with timestamps to correct.
* `--timestamp-format` - format of timestamps in the target timestamp attribute, see [Timestamp formats in the UXIsk Filters Framework](https://github.com/uxifiit/Filters#timestamp-formats) for supported values. 
* `--reference-timestamp-field` - name of the attribute with reference timestamps to use for correction.
* `--reference-timestamp-format` - similar to `--timestamp-format` but for reference timestamps. 
* `--log-format` - format of the log output, either CSV or JSON, can be omitted if the log file is set with these extensions.
* `--log` - path to the log file, if omitted, the standard error stream is used.
* `--output` - path to the output file in the same format as input (set with `--format`).

Use `--help` option to see all available options.

## Authors

* Martin Konopka - [@martinkonopka](https://github.com/martinkonopka)

## License

This project is licensed under the 3-Clause BSD License - see the [LICENSE.txt](..\..\LICENSE.txt) file for details

Copyright (c) 2019 Martin Konopka and Faculty of Informatics and Information Technologies, Slovak University of Technology in Bratislava.

## Contacts

* UXIsk 
  * User eXperience and Interaction Research Center
  * Faculty of Informatics and Information Technologies, Slovak University of Technology in Bratislava
  * Web: https://www.uxi.sk/
* Martin Konopka
  * E-mail: martin (underscore) konopka (at) stuba (dot) sk
