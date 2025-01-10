# IronPdf-Web-Api
A Web API project in C# .NET Core that can be used to integrate IronPdf with any other web client via REST
<br>
<h2>Instructions for Running the project:</h2><br>
1. Install .NET 8.0<br>
2. Install Visual Studio<br>
3. Clone the project in local machine and run the "IronPdf-Web-Api.sln" project in Visual Studio.<br>
4. Make sure to set the IronLicenseKey variable with your license key of IronPdf in the Constants.cs file.<br>
5. Run the project in Debug Mode.<br>
6. Backend url: https://localhost:5001<br>
7. Example API Url for trying out in Curl/Postman: https://localhost:5001/api/pdf/generatePdfFromHtmlString<br>
<br>
<h2>Authentication Controller APIs: </h2> 
<br>
Endpoint-api/auth/token
<br>
Function- API call to generate a JWT token for user authentication.
<br>
<br>
<h2>Pdf Controller APIs:</h2>
<br>
Endpoint-api/pdf/generatePdfFromHtmlString
<br>
Function- API call to generate a Pdf from Html String using IronPDF.
<br>
<br>
Endpoint- api/pdf/generatePdfFromUrl
<br>
Function- API call to generate a Pdf from a webpage Url using IronPDF.
<br>
<br>
Endpoint-api/pdf/extractPdfAsImages
<br>
Function- API call to download a stored Pdf as images (one image per page).
