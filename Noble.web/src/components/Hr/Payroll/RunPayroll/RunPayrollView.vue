<template>
    <modal :show="show" v-if="isValid('CanViewBankRunPayroll') || isValid('CanViewCashRunPayroll')" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" >{{ $t('PayrollRunView.PayrollRunView') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class=" table-responsive mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th style="background-color: darkseagreen;">#</th>
                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.Employee') }}
                                    </th>
                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.BaseSalary') }}
                                    </th>

                                    <th style="background-color: antiquewhite;" class="text-center" v-for="(allowance ,index) in runPayroll.allowanceHeader" v-bind:key="index+'allowanceHeader'">
                                        <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            {{allowance.nameInPayslip}}
                                        </span> 
                                        <span v-else>
                                            {{allowance.nameInPayslipArabic}}
                                        </span> 
                                    </th>

                                    <th v-if="runPayroll.hourly" class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.HourAmount') }}
                                    </th>

                                    <th style="background-color: #ffc7c7;" class="text-center" v-for="(deduction ,index) in runPayroll.deductionHeader" v-bind:key="index+'deductionHeader'">
                                        <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            {{deduction.nameInPayslip}}
                                        </span>
                                        <span v-else>
                                            {{deduction.nameInPayslipArabic}}
                                        </span>
                                    </th>

                                    <th style="background-color: #dee9e8;" class="text-center" v-for="(contribution ,index) in runPayroll.contributionHeader" v-bind:key="index+'contributionHeader'">
                                        <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            {{contribution.nameInPayslip}}
                                        </span>
                                        <span v-else>
                                            {{contribution.nameInPayslipArabic}}
                                        </span>
                                    </th>

                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.IncomeTax') }}
                                    </th>
                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.Loan') }}
                                    </th>
                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.NetSalary') }}
                                    </th>
                                    <th class="text-center" style="background-color: darkseagreen;">

                                    </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(salaryTemplate ,index) in runPayroll.salaryTemplateList" v-bind:key="salaryTemplate.id">
                                    <td>
                                        {{index+1}}
                                    </td>
                                    <td>
                                        {{salaryTemplate.employeeEnglishName}}
                                    </td>
                                    
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
                                    <td class="text-center">
                                        <a href="javascript:void(0);" v-on:click="GetPaySlip(salaryTemplate.employeeId)"  v-if="isValid('CanViewCashRunPayroll')"><i class="fas fa-print text-secondary font-16"></i></a>

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
                                        {{parseFloat(deduction.amount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                    </th>

                                    <th class="text-center" v-for="(contribution ,index) in runPayroll.contributionFooter" v-bind:key="index+'contributionFoot'">
                                        {{parseFloat(contribution.amount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
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
                                    <td class="text-center">
                                    </td>
                                </tr>

                                </tbody>
                            </table>
                        </div>
                       
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="PrintCsv()"  v-if="isValid('CanDownloadCsvRunPayroll')">{{ $t('PayrollRunView.ExportCsv') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="Print()"  v-if="isValid('CanDownloadPdfRunPayroll')">{{ $t('PayrollRunView.ExportPdf') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('PayrollRunView.Cancel') }}</button>
            </div>
            <run-payroll-report-print :runPayroll="runPayroll" v-if="isPrint" v-bind:key="printRender" />
            <payslip-print :payslip="payslip" :headerFooter="headerFooter" v-if="isPaySlipPrint" v-bind:key="payslipRender" />
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>




    <!-- <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="isValid('CanViewBankRunPayroll') || isValid('CanViewCashRunPayroll')">
        <modal :show="show" :modalLarge="true" >
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h5 class="modal-title DayHeading text-center mb-3" id="myModalLabel">{{ $t('PayrollRunView.PayrollRunView') }} </h5>
                        <table class="table">
                            <thead v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                <tr>
                                    <th style="background-color: darkseagreen;">#</th>
                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.Employee') }}
                                    </th>
                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.BaseSalary') }}
                                    </th>

                                    <th style="background-color: antiquewhite;" class="text-center" v-for="(allowance ,index) in runPayroll.allowanceHeader" v-bind:key="index+'allowanceHeader'">
                                        <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            {{allowance.nameInPayslip}}
                                        </span> 
                                        <span v-else>
                                            {{allowance.nameInPayslipArabic}}
                                        </span> 
                                    </th>

                                    <th v-if="runPayroll.hourly" class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.HourAmount') }}
                                    </th>

                                    <th style="background-color: #ffc7c7;" class="text-center" v-for="(deduction ,index) in runPayroll.deductionHeader" v-bind:key="index+'deductionHeader'">
                                        <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            {{deduction.nameInPayslip}}
                                        </span>
                                        <span v-else>
                                            {{deduction.nameInPayslipArabic}}
                                        </span>
                                    </th>

                                    <th style="background-color: #dee9e8;" class="text-center" v-for="(contribution ,index) in runPayroll.contributionHeader" v-bind:key="index+'contributionHeader'">
                                        <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            {{contribution.nameInPayslip}}
                                        </span>
                                        <span v-else>
                                            {{contribution.nameInPayslipArabic}}
                                        </span>
                                    </th>

                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.IncomeTax') }}
                                    </th>
                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.Loan') }}
                                    </th>
                                    <th class="text-center" style="background-color: darkseagreen;">
                                        {{ $t('PayrollRunView.NetSalary') }}
                                    </th>
                                    <th class="text-center" style="background-color: darkseagreen;">

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
                                    <td class="text-center">
                                        <a href="javascript:void(0)" v-on:click="GetPaySlip(salaryTemplate.employeeId)" class="btn btn-icon btn-round btn-primary btn-sm"><i class="fa fa-print"></i></a>
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
                                        {{parseFloat(deduction.amount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                    </th>

                                    <th class="text-center" v-for="(contribution ,index) in runPayroll.contributionFooter" v-bind:key="index+'contributionFoot'">
                                        {{parseFloat(contribution.amount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
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
                                    <td class="text-center">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="modal-footer justify-content-right">
                <button type="button" class="btn btn-primary  mr-3 " v-on:click="PrintCsv()" v-if="isValid('CanDownloadCsvRunPayroll')">{{ $t('PayrollRunView.ExportCsv') }}</button>
                <button type="button" class="btn btn-primary  mr-3 " v-on:click="Print()" v-if="isValid('CanDownloadPdfRunPayroll')">{{ $t('PayrollRunView.ExportPdf') }}</button>
                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('PayrollRunView.Cancel') }}</button>
            </div>
            <run-payroll-report-print :runPayroll="runPayroll" v-if="isPrint" v-bind:key="printRender" />
            <payslip-print :payslip="payslip" :headerFooter="headerFooter" v-if="isPaySlipPrint" v-bind:key="payslipRender" />
        </modal>
    </div>
    <div v-else>
        <acessdenied  :model=true></acessdenied>
    </div> -->
</template>
<script>
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        props: ['show', 'runPayroll'],
        mixins: [clickMixin],
         components: {
            Loading
        },
        data: function () {
            return {
                 loading: false,
                isPaySlipPrint: false,
                isPrint: false,
                printRender: 0,
                payslipRender: 0,
                payslip: '',

                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
            }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },

            Print: function () {
                
                this.isPrint = true;
                this.printRender++;
            },


            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
                       
                    });

            },
            GetHeaderDetail: function () {
                var root = this;                
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.headerFooter.company = response.data;                            
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },

            GetPaySlip: function (employeeId) {
                
                this.isPrint = false;
                var root = this;
                this.GetHeaderDetail();
                root.$https.get('/Payroll/GetPaySlip?employeeId=' + employeeId + '&runPayrollId=' + this.runPayroll.id, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.payslip = response.data;
                            root.isPaySlipPrint = true;
                            root.payslipRender++;
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },

            PrintCsv: function () {
                var root = this;
                
                root.$https.post('/Payroll/RunPayrollDetailPrintCsv', this.runPayroll, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                    .then(function (response) {
                        
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'PayrollSheet.csv');
                        document.body.appendChild(link);
                        link.click();

                    });
            },
        },
        mounted: function () {
        }
    }
</script>
