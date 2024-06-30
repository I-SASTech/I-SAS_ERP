<template>
    <div>
        <div hidden id='inventoryDetailReport'>
            <!--INFORMATION-->
            <div class="row" style="height:40mm;">
                <div class="col-md-6">
                    <img :src="headerFooter.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                </div>
                <div class="col-md-6" style="color:#000000 !important; font-size:18px;">
                    <p class="text-right mb-0">{{headerFooter.company.phoneNo}}</p>
                    <p class="text-right mb-0">{{headerFooter.company.companyEmail}}</p>
                    <p class="text-right mb-0">{{headerFooter.company.companyNameArabic}}</p>
                    <p class="text-right mb-0">{{headerFooter.company.companyNameEnglish}}</p>
                    <p class="text-right mb-0">{{headerFooter.company.addressEnglish}}</p>
                    <p class="text-right mb-0">{{headerFooter.company.addressArabic}}</p>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <hr class="mt-3 mb-3" />
                </div>
            </div>

            <div class="row mt-5">
                <div class="col-md-12">
                    <h4>Payslip For the month of {{payslip.salaryPeriod}}</h4>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <h5 style="background-color: #3DC26F; padding-bottom: 10px; padding-top: 5px;color:white;font-size:25px;">Employee Info</h5>
                </div>
            </div>

            <div class="row" style="height:55mm;">
                <div class="col-md-6">
                    <table class="table table-borderless" style="color:#000000 !important; font-size:18px;">
                        <tbody>
                            <tr>
                                <td>
                                    Employee Code:
                                </td>
                                <td class="text-right">
                                    {{payslip.employeeCode}}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Employee Name:
                                </td>
                                <td class="text-right">
                                    {{payslip.employeeEnglishName}}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Department:
                                </td>
                                <td class="text-right">
                                    {{payslip.employeeDepartment}}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Designation:
                                </td>
                                <td class="text-right">
                                    {{payslip.employeeDesignation}}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-md-6">
                    <table class="table table-borderless" style="color:#000000 !important; font-size:18px;">
                        <tbody>
                            <tr>
                                <td>
                                    Gender:
                                </td>
                                <td class="text-right">
                                    {{payslip.employeeGender}}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Joining Date:
                                </td>
                                <td class="text-right">
                                    {{payslip.employeeJoiningDate}}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    CNIC:
                                </td>
                                <td class="text-right">
                                    {{payslip.employeeIDNumber}}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Employee Type:
                                </td>
                                <td class="text-right">
                                    
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="row mt-5">
                <div class="col-md-4">
                    <h5 style="background-color: #3DC26F; padding-bottom: 10px; padding-top: 5px;color:white;font-size:25px;">Payment & allownace</h5>
                </div>

                <div class="col-md-2">
                </div>

                <div class="col-md-4">
                    <h5 style="background-color: #3DC26F; padding-bottom: 10px; padding-top: 5px;color:white;font-size:25px;">Deductions</h5>
                </div>
            </div>

            <div class="row" style="height:90mm;">
                <div class="col-md-6">
                    <table class="table table-borderless" style="color:#000000 !important; font-size:18px;">
                        <tbody>
                            <tr>
                                <td style="width:40%;">
                                    Basic Salary:
                                </td>
                                <td style="width:60%;" class="text-right">
                                    {{payslip.baseSalary}}
                                </td>
                            </tr>
                            <tr v-for="allowance in payslip.allowanceList" :key="allowance.id">
                                <td>
                                    {{allowance.nameInPayslip}}
                                </td>
                                <td class="text-right">
                                    {{allowance.amount}}
                                </td>
                            </tr>
                            <tr v-if="payslip.salaryType=='Hourly Based' || payslip.salaryType=='بالساعة'">
                                <td>
                                    Hour Amount ({{payslip.hour}} * {{payslip.weekdayHourlySalary}}):
                                </td>
                                <td class="text-right">
                                    {{payslip.hourAmount}}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Gross Pay:
                                </td>
                                <td class="text-right">
                                    {{payslip.grossSalary}}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Net Pay In Words::
                                </td>
                                <td class="text-right">
                                    {{$toWords(payslip.netSalary) }}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-md-6">
                    <table class="table table-borderless" style="color:#000000 !important; font-size:18px;">
                        <tbody>
                            <tr style="width:40%;" v-for="deduction in payslip.deductionList" :key="deduction.id">
                                <td>
                                    {{deduction.nameInPayslip}}
                                </td>
                                <td style="width:60%;" class="text-right">
                                    {{deduction.amount}}
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    Total Deductions:
                                </td>
                                <td class="text-right">
                                    {{payslip.deductionAmount}}
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    Income Tax Amount:
                                </td>
                                <td class="text-right">
                                    {{payslip.incomeTaxAmount}}
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    Net Pay In Figures:
                                </td>
                                <td class="text-right">
                                    {{payslip.netSalary}}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="row mt-5">
                <div class="col-md-4">
                    <h5 style="background-color: #3DC26F; padding-bottom: 10px; padding-top: 5px;color:white;font-size:25px;">Loan & Advances</h5>
                </div>

                <div class="col-md-2">
                </div>

                <div class="col-md-4">
                    <h5 style="background-color: #3DC26F; padding-bottom: 10px; padding-top: 5px;color:white;font-size:25px;">Contribution</h5>
                </div>
            </div>

            <div class="row" style="height:75mm;">
                <div class="col-md-6">
                    <table class="table table-borderless" style="color:#000000 !important; font-size:18px;">
                        <tbody>
                            <tr>
                                <td style="width:40%;">
                                    Loan Amount:
                                </td>
                                <td style="width:60%;" class="text-right">
                                    {{payslip.loanAmount}}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-md-6">
                    <table class="table table-borderless" style="color:#000000 !important; font-size:18px;">
                        <tbody>
                            <tr style="width:40%;" v-for="contribution in payslip.contributionList" :key="contribution.id">
                                <td>
                                    {{contribution.nameInPayslip}}
                                </td>
                                <td style="width:60%;" class="text-right">
                                    {{contribution.amount}}
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    Total contribution:
                                </td>
                                <td class="text-right">
                                    {{payslip.contributionAmount}}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>


            <div class="row">
                <div class="col-md-10 ml-auto mr-auto">
                    <p class="text-center" style="color:#000000;font-size:18px;">This is a computer generated statement and does not require a stamp or signature. This is a confidentail document and is not to be shared or disclosed to anyone. For any inquries email within 2 days.</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    //import moment from "moment";
    const options = {
        name: '',
        specs: [
            'fullscreen=no',
            'titlebar=yes',
            'scrollbars=yes'
        ],
        styles: [
            'https://unpkg.com/kidlat-css/css/kidlat.css',
            './assets/css/bootstrap.min.css' // <- inject here
        ],
        timeout: 700,
        autoClose: true,
        windowTitle: window.document.title,
    }

    export default {
        props: ['payslip','headerFooter'],
        data: function () {
            return {
                language: '',

            }
        },
        filters: {
            toWords: function (value) {
                var converter = require('number-to-words');
                if (!value) return ''
                return converter.toWords(value);
            }
        },
        mounted: function () {
            
            console.log(this.payslip);

            var root = this;
            this.language = this.$i18n.locale;

            setTimeout(function () {
                root.printInvoice();
            }, 125)
        },
        methods: {
            printInvoice: function () {
                this.$htmlToPaper('inventoryDetailReport', options, () => {
                    console.log('No Rander the Page');
                });
            }
            
        }
    }
</script>
