# Project 1

In this tutorial, we are tasked with creating an application that reads a CSV file containing a list of students, filters out duplicate and incorrect records, and saves the filtered data in a JSON format. The application follows a specific set of requirements:

1. The application takes two arguments: the location of the input CSV file and the location of the output JSON file.
2. It reads the student data from the input file.
3. Duplicate students are filtered based on their index number, first name, last name, direction of study, and mode of study.
4. If a duplicate student is found with different studies, the new studies are appended to the existing record.
5. Erroneous records, which do not consist of 9 fields, are logged in a `log.txt` file.
6. The output JSON file includes a timestamp of when it was created, the author's name, and a list of filtered student records.
7. Each student record includes an index number starting with the letter 's', name, surname, email, birth date, parents' names, and a list of their studies.
