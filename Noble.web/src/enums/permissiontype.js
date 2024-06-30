import modules from "./modules";

const permissiontype = Object.freeze({

    //Setting And Permission
    'UserRole': { value: "User Role", id: '30967fc6-c7c6-4c68-b9cd-6a4b259e2ae5', moduleId: modules.SettingsAndPermission.id },
    'UserPermission': { value: "User Permission", id: '5cd7723f-3b03-48e4-84fb-fdc20248cee9', moduleId: modules.SettingsAndPermission.id },
    'SignUp': { value: "SignUp User", id: '578f0de7-5ba3-46b1-bf73-5e955c714af5', moduleId: modules.SettingsAndPermission.id },

    //Sale Module
    'Customer': { value: "Customer/Client", id: '95d12b39-b55f-4d34-b89f-ee5822004e59', moduleId: modules.Sales.id },
    'SaleRecord': { value: "Sale Record", id: '711b1378-4afc-4e7e-b2c3-e6b12e5c75ca', moduleId: modules.Sales.id },
    'AddInvoice': { value: "Add Invoice", id: '7bd17213-9af6-4748-8941-fb2f2b86d2fa', moduleId: modules.Sales.id },
    'TouchScreen': { value: "Touch Screen", id: 'b4ecdb41-f403-4f12-8483-38778441daab', moduleId: modules.Sales.id },
    'SaleReturn': { value: "Sale Return", id: '8b80ee35-a302-4f42-a802-51e7b3afde96', moduleId: modules.Sales.id },
    'SaleOrder': { value: "Sale Order", id: '7fc8f9ca-af0d-4b3d-9504-9fa0cdd0f61d', moduleId: modules.Sales.id },
    'SaleOrderTracking': { value: "Sale Order Tracking", id: '495050bb-7880-4a7b-8b16-98ac57f46d6c', moduleId: modules.Sales.id },
    'Quotation': { value: "Quotation", id: '7aca388e-2870-4579-ba0c-f39975ab11f6', moduleId: modules.Sales.id },
    'InvoiceSetting': { value: "Invoice Setting", id: 'bfeadf6a-57a1-486e-bd09-3cc65d1a0cc7', moduleId: modules.Sales.id },
    'CustomerPayReceipt': { value: "Customer Pay Receipt", id: '9764c3a1-0b75-4b2a-8acb-fad824ce7427', moduleId: modules.Sales.id },

    'SaleServiceRecord': { value: "Sale Service Record", id: '73494f01-8751-4ccd-af8d-967056a9fe5d', moduleId: modules.Sales.id },
    'AddServiceInvoice': { value: "Add Service Invoice", id: 'e6eda0a2-1ac6-4ff4-a34e-e6fad051e897', moduleId: modules.Sales.id },
    'SaleServiceOrder': { value: "Sale Service Order", id: '77b1950a-7430-41c5-a0c8-367cdb4a2e70', moduleId: modules.Sales.id },
    'ServiceQuotation': { value: "Service Quotation", id: '7fc15d15-f0b1-4cdc-89f4-c8d05ed48602', moduleId: modules.Sales.id },

    'AdvanceReceipt': { value: "Advance Receipt", id: 'a91382cb-9d03-46c3-8d9a-b4c034bc14b5', moduleId: modules.Sales.id },

    'StuffingLocation': { value: "Stuffing Location", id: 'd9cc3fc1-51cf-4e08-ab8d-c442ff833ecc', moduleId: modules.Sales.id },
    'PartOfLoading': { value: "Part Of ", id: '34f8396a-4cbf-4491-b7cf-d03b0a1c916b', moduleId: modules.Sales.id },
    'PartOfDestination': { value: "Part Of Destination", id: '62685c87-dd6e-41e1-b1a8-15243c861542', moduleId: modules.Sales.id },
    'OrderType': { value: "Order Type", id: '0b0baa0a-2bb4-46d7-9038-617e48f14f87', moduleId: modules.Sales.id },
    'Service': { value: "Service", id: '397ec615-9cbe-4952-a7b6-fc0ae1ea892c', moduleId: modules.Sales.id },
    'Incoterms': { value: "Incoterms", id: '805fd7a0-e221-47e6-8792-aef781affe43', moduleId: modules.Sales.id },
    'Commodity': { value: "Commodity", id: '5bed86ee-c66a-4d92-9f82-df4c85dbcc11', moduleId: modules.Sales.id },
    'Carrier': { value: "Carrier", id: 'b9e7cad8-87db-4347-9aeb-088ae706cf41', moduleId: modules.Sales.id },
    'ExportExw': { value: "Export Exw", id: '327ccb82-2435-47fd-80bd-a240ac09c77a', moduleId: modules.Sales.id },
    'ImportFOB': { value: "Import FOB", id: '7c40f186-0e7d-4769-8661-587921373482', moduleId: modules.Sales.id },
    'QuantityContainer': { value: "Quantity Container", id: 'b021868c-f46d-424c-8ee8-88381f6fe78c', moduleId: modules.Sales.id },
    'CustomSetting': { value: "Custom Setting", id: '19f269f7-79af-4d5a-9271-2a9314941691', moduleId: modules.Sales.id },
    'QuotationTemplate': { value: "Quotation Template", id: '022add04-28bd-4833-99fe-c6659ff0da99', moduleId: modules.Sales.id },
    'QuotationServiceTemplate': { value: "Quotation Service Template", id: 'b0b76e04-827c-4916-8588-d4b449580be9', moduleId: modules.Sales.id },
    'ProformaInvoice': { value: "Proforma Invoice", id: 'fde4c4fd-c589-47f3-bf5f-4f1e8c38672e', moduleId: modules.Sales.id },
    'DeliveryNote': { value: "Delivery Note", id: '1d77f9ef-01a1-4825-8bc0-e2af3dc8262b', moduleId: modules.Sales.id },
    'PricingOnSale': { value: "Pricing On Sale", id: '6a44cb9d-263c-45a3-ab7d-e2e3b3a56003', moduleId: modules.Sales.id },


    
    //Purchase Module 
    
    'PurchaseInvoice': { value: "Purchase Invoice", id: '63c3ff04-e92c-481d-b52c-57f1d1189756', moduleId: modules.Purchase.id },
    'PurchaseOrder': { value: "Purchase Order", id: 'c615c8fc-7632-42d0-8804-248558fa522d', moduleId: modules.Purchase.id },
    'AutoPurchaseTemplate': { value: "Auto Purchase Template", id: 'aaa8aaa0-d411-4fde-b004-9791de6606f5', moduleId: modules.Purchase.id },
    'PurchaseReturn': { value: "Purchase Return", id: 'd55fa36d-d132-4f7b-b095-ca01449b2c6a', moduleId: modules.Purchase.id },
    'SupplierQuotation': { value: "Supplier Quotation", id: '2851b7c3-d3cf-4e81-85b5-702cabc6e13b', moduleId: modules.Purchase.id },
    'Supplier': { value: "Supplier", id: 'b0b8cbb6-1b87-47dd-a57c-0d2d6dfa1ef9', moduleId: modules.Purchase.id },
    'SupplierPayReceipt': { value: "Supplier Pay Receipt", id: '1f4bb5ca-53ef-4e06-8fc1-552b5d4f99c4', moduleId: modules.Purchase.id },
    'GoodsReceive': { value: "Goods Receive", id: '8f4eb5ca-57ef-4e06-8dc1-559b5d4f99c4', moduleId: modules.Purchase.id },
    'ReparingOrder': { value: "Reparing Order", id: '134ac47e-9b01-4966-a4b7-243483fcac2a', moduleId: modules.Purchase.id },

    'WarrantyCategory': { value: "Warranty Category", id: 'cb1ff3f9-cc16-4573-ad8a-1f6e42038415', moduleId: modules.Purchase.id },
    'Description': { value: "Description", id: 'eecdd3bd-8946-4268-8618-0a6fdcab1007', moduleId: modules.Purchase.id },
    'Problem': { value: "Problem", id: '63a5f7b0-70e0-4558-b2f9-d9d337db3850', moduleId: modules.Purchase.id },
    'Accessory': { value: "Accessory", id: 'b8446127-9aa5-4728-9367-499884cb65c9', moduleId: modules.Purchase.id },
    



    // Accounting Module

    'PettyCash': { value: "Petty Cash", id: 'bddbc35f-cd90-478d-87e2-74857a1d331d', moduleId: modules.Accounting.id },
    'JournalVoucher': { value: "Journal Voucher", id: 'a12811af-8a30-4575-adc3-f04e593521c1', moduleId: modules.Accounting.id },
    'OpeningCash': { value: "Opening Cash", id: '2eb0eac7-9446-4702-bb71-ab937a61035f', moduleId: modules.Accounting.id },
    'TemporaryCashRequest': { value: "Temporary Cash Request", id: '00024d39-0ef4-496b-af33-069230af09aa', moduleId: modules.Accounting.id },
    'TemporaryCashIssue': { value: "Temporary Cash Issue", id: '3348474f-6d23-41e7-9273-074f355a0206', moduleId: modules.Accounting.id },
    'TemporaryCashAllocation': { value: "Temporary Cash Allocation", id: '07c59895-d6ad-40bd-95b0-90f9032afd72', moduleId: modules.Accounting.id },
    'Currency': { value: "Currency", id: '266c8de0-c959-49e8-a7e6-9d26f5192ad8', moduleId: modules.Accounting.id },
    'Bank': { value: "Bank", id: 'db64ef97-e939-4dab-9e56-7622dd1a03fa', moduleId: modules.Accounting.id },
    'MonthlyCost': { value: "Monthly Cost", id: '80a6e2bf-2101-44ae-98ea-f84745de4a9d', moduleId: modules.Accounting.id },
    'ChartOfAccount': { value: "Chart Of Account", id: '7e0716c2-ac50-47a5-b302-6efab5653f5b', moduleId: modules.Accounting.id },
    'PaymentOptions': { value: "Payment Options", id: '1f2d3feb-5f7d-4f5d-a15a-bbed15b63e03', moduleId: modules.Accounting.id },
    'DenominationSetup': { value: "Denomination Setup", id: 'bf2d89cf-be67-4d19-a573-ffe39d87a876', moduleId: modules.Accounting.id },
    'VatRate': { value: "Vat Rate", id: '0df79aaa-4e0a-40e7-840d-416f9f2d4993', moduleId: modules.Accounting.id },
    'ExpenseType': { value: "Expense Type", id: '34c2f07d-abd6-4772-a7e2-9c7491e9b040', moduleId: modules.Accounting.id },
    'FinancialYear': { value: "Financial Year", id: 'cbb58200-aa4c-40fb-85ea-dca7e8fd2e81', moduleId: modules.Accounting.id },
    'ChequesAndGuarantees': { value: "Cheques And Guarantees", id: '94eb8d8b-9285-4e29-a6ee-5c7812b882ca', moduleId: modules.Accounting.id },

    //Reporting

    'Reporting': { value: "Reporting", id: '0b7085cd-ee03-4c6a-bbac-33f5ed9d29fd', moduleId: modules.Reporting.id },

    //Expense Module
    'DailyExpense': { value: "Daily Expense", id: '1cf0bb1e-e47c-4b23-92c6-af7dde72452f', moduleId: modules.Expenses.id },
    'ExpenseBill': { value: "Expense Bill", id: '35b3ca64-03bc-4134-8268-e11eb6e95fa2', moduleId: modules.Expenses.id },

    //Inventory Module
    'Product': { value: "Product", id: '74ae9960-54b5-48c2-94a4-fef82501f56f', moduleId: modules.ProductAndInventoryManagement.id },
    'Category': { value: "Category", id: '3213f85e-7965-4daf-bd5b-280aeea462d6', moduleId: modules.ProductAndInventoryManagement.id },
    'SubCategory': { value: "SubCategory", id: '0351dbd1-0a91-403d-9ebe-35af855d6390', moduleId: modules.ProductAndInventoryManagement.id },
    'Brand': { value: "Brand", id: '832a7bba-74cc-40e9-bf84-f278f202f650', moduleId: modules.ProductAndInventoryManagement.id },
    'Origin': { value: "Origin", id: '461d63f6-4165-41f3-958e-0d4777a64f8b', moduleId: modules.ProductAndInventoryManagement.id },
    'Size': { value: "Size", id: '62164de1-99bc-40a1-b7f7-ec1fe8771f91', moduleId: modules.ProductAndInventoryManagement.id },
    'Color': { value: "Color", id: '3d1a2d82-bf33-4345-926d-f8ba020fe4c8', moduleId: modules.ProductAndInventoryManagement.id },
    'Unit': { value: "Unit", id: '9c411469-7c6f-4a32-9b0e-effce6b79b98', moduleId: modules.ProductAndInventoryManagement.id },
    'Item': { value: "Item", id: 'a9469e63-abdd-4744-9d46-970540d205f3', moduleId: modules.ProductAndInventoryManagement.id },
    'InventoryCount': { value: "Inventory Count", id: '85b7b2c7-ab1f-426e-818b-1169c2aae492', moduleId: modules.ProductAndInventoryManagement.id },
    'PromotionOffer': { value: "Promotion Offer", id: '56545aa0-3f5a-4e1b-bbbb-fad29fd4e5e3', moduleId: modules.ProductAndInventoryManagement.id },
    'BundleOffer': { value: "Bundle Offer", id: '85e8301b-e609-4c1c-9c60-89eca4473e0c', moduleId: modules.ProductAndInventoryManagement.id },
    'BarcodeSetup': { value: "Barcode Setup", id: 'b2e58d36-85c9-48f3-b39b-deab8d8ae336', moduleId: modules.ProductAndInventoryManagement.id },
	
    'WarrantyType': { value: "Warranty Type", id: 'd9889108-9398-4e0f-b048-689c479d3e5a', moduleId: modules.ProductAndInventoryManagement.id },



    //WareHouse Management Module
    'WareHouse': { value: "WareHouse", id: '988fdb69-a464-4a4f-8e5e-9c8ab09d07db', moduleId: modules.WareHouseManagement.id },
    'StockIn': { value: "Stock In", id: 'bb6595aa-874f-4b6f-adbe-a9ff2720ba44', moduleId: modules.WareHouseManagement.id },
    'StockOut': { value: "Stock Out", id: '5779b081-0946-46aa-bbd6-d5971ba647e1', moduleId: modules.WareHouseManagement.id },
    'StockTransfer': { value: "Stock Transfer", id: '13e4798c-c28f-4d0c-a30e-bba62c5ffbac', moduleId: modules.WareHouseManagement.id },

    //Logistic Module

    'Transporter': { value: "Transporter", id: 'dc501440-24b8-47cf-9006-8e4123f505a2', moduleId: modules.Logistic.id },
    'ClearanceAgent': { value: "Clearance Agent", id: '51880a01-5e17-4e9d-b939-6f22db3bc2db', moduleId: modules.Logistic.id },
    'ShippingLiner': { value: "Shipping Liner", id: '7517c001-9b9e-4e59-91da-c8cfc073a6a4', moduleId: modules.Logistic.id },


    //Setup And Configuration


    'CompanyInfo': { value: "Company Info", id: '275b93f8-69da-480b-ade9-6ba7295a7066', moduleId: modules.SetupsAndConfiguration.id },
    'BackUpAndRestore': { value: "BackUp And Restore", id: '492b1b1b-2250-4a9d-b65e-aed60337447c', moduleId: modules.SetupsAndConfiguration.id },
    'Synchronization': { value: "Synchronization", id: '10d3c578-7da0-478a-84c1-24e49e484694', moduleId: modules.SetupsAndConfiguration.id },
    'ResetDatabase': { value: "Reset Database", id: 'dccdc160-09cc-4ffd-a4dc-58a6ccbc6a4f', moduleId: modules.SetupsAndConfiguration.id },
    

    //DayStart Module

    'SystemTerminal': { value: "System Terminal", id: '44227c17-1cab-4658-a036-4d42820b14a3', moduleId: modules.DayStart.id },
    'SystemPosTerminal': { value: "System Pos Terminal", id: 'dccdc160-09cc-4ffd-a4dc-58a6ccbc6a4f', moduleId: modules.DayStart.id },
    'StartOperation': { value: "Start Operation", id: '5b550db9-5215-404b-ac26-e5e868c0694a', moduleId: modules.DayStart.id },

    
    

    //Production Module
    'ProductionRecipes': { value: "Production Recipes", id: 'b5b0d0f1-2df2-4db0-b46b-d6089792981e', moduleId: modules.ManufacturingAndProduction.id },
    'ProductionBatch': { value: "Production Batch", id: 'bb308397-ef74-4884-bee9-76ba0fa2df9a', moduleId: modules.ManufacturingAndProduction.id },
    'DispatchNote': { value: "Dispatch Note", id: 'f4fbf5b8-fbf4-482e-bc47-21f1b5aaa62f', moduleId: modules.ManufacturingAndProduction.id },
    'General': { value: "General", id: '345a11a5-8103-4f72-a955-94b418986426', moduleId: modules.ManufacturingAndProduction.id },
	
	
	//Hr and Payroll Module
    'AllowanceType': { value: "Allowance Type", id: 'f0764cda-655f-4210-a61e-50a4c971e9e0', moduleId: modules.HrAndPayRoll.id },
    'Allowance': { value: "Allowance", id: 'fb6eeeb1-2334-4843-866d-a44ac6395fd5', moduleId: modules.HrAndPayRoll.id },
    'Deduction': { value: "Deduction", id: 'd825a7d1-feb1-477e-8304-0694a1b2851a', moduleId: modules.HrAndPayRoll.id },
    'Contribution': { value: "Contribution", id: '0a6cda1b-ad4c-4049-ac44-1000e47059c8', moduleId: modules.HrAndPayRoll.id },
    'PayRollSchedule': { value: "PayRoll Schedule", id: '68cc9fc0-7849-4d4b-8ba0-1512427bfe86', moduleId: modules.HrAndPayRoll.id },
    'SalaryTemplate': { value: "Salary Template", id: '665d5caf-1565-4973-b7a8-fd49a508c208', moduleId: modules.HrAndPayRoll.id },
    'EmployeeSalary': { value: "Employee Salary", id: '60108f71-b36d-475b-a7fe-3531d745d571', moduleId: modules.HrAndPayRoll.id },
    'LoanPayment': { value: "Loan Payment", id: '8229fba7-4cdc-42d1-976f-2f295b1c7a42', moduleId: modules.HrAndPayRoll.id },
    'SalaryTaxSlab': { value: "Salary Tax Slab", id: '5ad4fed4-4059-4ab8-8a36-a98755810860', moduleId: modules.HrAndPayRoll.id },
    'RunPayroll': { value: "Run Payroll", id: '3f7722f2-1c93-4c78-8a23-8aaafd835e87', moduleId: modules.HrAndPayRoll.id },
    'EmployeeRegistration': { value: "Employee Registration", id: 'b97a70d4-2865-4573-8e0b-c4dea323d8c1', moduleId: modules.HrAndPayRoll.id },
    'ManualAttendance': { value: "ManualAttendance", id: 'aa2ece3a-2e52-47e3-a713-25cd916a30f7', moduleId: modules.HrAndPayRoll.id },
    //'Designation': { value: "Designation", id: 'ca76eae1-6759-49e6-8af7-c7bc936a353d', moduleId: modules.HrAndPayRoll.id },
    //'Department': { value: "Department", id: '0b47ce4d-e1a6-4f9e-95f0-981b08957d9e', moduleId: modules.HrAndPayRoll.id }
    'LeavesManagement': { value: "LeavesManagement", id: '7ab9feb4-3c23-415c-86d6-a03d87af3f01', moduleId: modules.HrAndPayRoll.id },


    //Inquiry Management Module
    'InquiryProcess': { value: "Inquiry Process", id: '3d18af30-8bfe-4d86-abc4-9763ba5a9db0', moduleId: modules.InquiryManagement.id },
    'InquiryType': { value: "Inquiry Type", id: 'f233c5d8-1398-49da-a36a-c17de0f28347', moduleId: modules.InquiryManagement.id },
    'InquirySetup': { value: "Inquiry Setup", id: 'ed0571d6-9c9d-4bf1-9d98-18b3530b26df', moduleId: modules.InquiryManagement.id },
    'Inquiry': { value: "Inquiry", id: '1f8cb90d-aa3f-49a7-989d-30aca0a89eb3', moduleId: modules.InquiryManagement.id },
    'InquiryPriority': { value: "Inquiry Priority", id: 'b522f087-ec47-442a-8633-2c496518b5e9', moduleId: modules.InquiryManagement.id },
    'HearedAbout': { value: "Heared About", id: 'd3f73e5b-71c0-4d51-8372-869e8a8b2d4c', moduleId: modules.InquiryManagement.id },

     //Dashboard
     'Dashboard': { value: "Dashboard", id: 'd8d80043-e93e-4282-a082-d19de69c02aa', moduleId: modules.Dashboard.id },
    
    



});

export default permissiontype;