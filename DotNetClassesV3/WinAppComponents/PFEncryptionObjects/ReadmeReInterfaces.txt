Keep the IFileEncryptor and IStringEncryptor interfaces and the classes that implement them (e.g. StringEncryptorAES, FileEncryptorAES).
The classes defined separately for each algorithm (AES, DES, TripleDES) are heavily used by the WinRegistrationComponents classes.
If you get rid of the individual classes then you have to modify all the WinRegistrationComponents classes. Recommendation is to not do this. It is not worth the time.
Use the PFFileEncryptor and PFStringEncryptor classes for new development (pfEncryptor app, database extract apps).