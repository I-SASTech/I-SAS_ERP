<template>
    <div>
        <div hidden id='inventoryDetailReport'>
            <!--INFORMATION-->
            <div class="row">
                <div class="col-md-12">
                    <h5 class="text-center">Payroll: {{runPayroll.payrollSchedule}}</h5>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <table class="table">
                        <thead v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                            <tr>
                                <th style="background-color: darkseagreen;">#</th>
                                <th class="text-center" style="background-color: darkseagreen;">
                                    Employee (English)
                                </th>
                                <th class="text-center" style="background-color: darkseagreen;">
                                    Base Salary
                                </th>

                                <th style="background-color: antiquewhite;" class="text-center" v-for="(allowance ,index) in runPayroll.allowanceHeader" v-bind:key="index+'allowanceHeader'">
                                    {{allowance.nameInPayslip}}
                                </th>

                                <th v-if="runPayroll.hourly" class="text-center" style="background-color: darkseagreen;">
                                    Hour Amount
                                </th>

                                <th style="background-color: #ffc7c7;" class="text-center" v-for="(deduction ,index) in runPayroll.deductionHeader" v-bind:key="index+'deductionHeader'">
                                    {{deduction.nameInPayslip}}
                                </th>

                                <th style="background-color: #dee9e8;" class="text-center" v-for="(contribution ,index) in runPayroll.contributionHeader" v-bind:key="index+'contributionHeader'">
                                    {{contribution.nameInPayslip}}
                                </th>

                                <th class="text-center" style="background-color: darkseagreen;">
                                    Income Tax
                                </th>
                                <th class="text-center" style="background-color: darkseagreen;">
                                    Loan
                                </th>
                                <th class="text-center" style="background-color: darkseagreen;">
                                    Net Salary
                                </th>
                            </tr>
                        </thead>
                        <tbody v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                            <tr v-for="(salaryTemplate ,index) in runPayroll.salaryTemplateList" v-bind:key="salaryTemplate.id">
                                <td>
                                    {{index+1}}
                                </td>
                                <td>
                                    {{salaryTemplate.employeeEnglishName}}
                                </td>
                                <!--<td>
                                    {{salaryTemplate.employeeArabicName}}
                                </td>-->
                                <td class="text-center">
                                    {{  parseFloat(salaryTemplate.baseSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>

                                <td class="text-center" v-for="(allowance ,index) in salaryTemplate.allowanceList" v-bind:key="index+'allowance'">
                                    {{  parseFloat(allowance.amount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>

                                <td v-if="runPayroll.hourly" class="text-center">
                                    {{  parseFloat(salaryTemplate.hourAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>

                                <td class="text-center" v-for="(deduction ,index) in salaryTemplate.deductionList" v-bind:key="index+'deduction'">
                                    {{  parseFloat(deduction.amount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>

                                <td class="text-center" v-for="(contribution ,index) in salaryTemplate.contributionList" v-bind:key="index+'contribution'">
                                    {{  parseFloat(contribution.amount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>

                                <td class="text-center">
                                    {{  parseFloat(salaryTemplate.incomeTaxAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>
                                <td class="text-center">
                                    {{  parseFloat(salaryTemplate.loanAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>
                                <td class="text-center">
                                    {{  parseFloat(salaryTemplate.netSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>
                            </tr>

                            <tr style="background-color: antiquewhite;" v-if="runPayroll.salaryTemplateList.length>0">
                                <td>
                                </td>
                                <td>
                                </td>
                                <td class="text-center">
                                    {{  parseFloat(runPayroll.totalBaseSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>

                                <th class="text-center" v-for="(allowance ,index) in runPayroll.allowanceFooter" v-bind:key="index+'allowanceFoot'">
                                    {{allowance.amount}}
                                </th>

                                <td v-if="runPayroll.hourly" class="text-center">
                                    {{  parseFloat(runPayroll.totalHourAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>

                                <th class="text-center" v-for="(deduction ,index) in runPayroll.deductionFooter" v-bind:key="index+'deductionFoot'">
                                    {{deduction.amount}}
                                </th>

                                <th class="text-center" v-for="(contribution ,index) in runPayroll.contributionFooter" v-bind:key="index+'contributionFoot'">
                                    {{contribution.amount}}
                                </th>

                                <td class="text-center">
                                    {{  parseFloat(runPayroll.totalIncomeTaxAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>
                                <td class="text-center">
                                    {{  parseFloat(runPayroll.totalLoanAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>
                                <td class="text-center">
                                    {{  parseFloat(runPayroll.totalNetSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>
                            </tr>
                        </tbody>
                    </table>
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
            'https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css',
            'https://unpkg.com/kidlat-css/css/kidlat.css',
            './assets/css/landscape.css' // <- inject here
        ],
        timeout: 700,
        autoClose: true,
        windowTitle: window.document.title,
    }

    export default {
        props: ['runPayroll'],
        data: function () {
            return {
                language: '',

            }
        },
        mounted: function () {
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
