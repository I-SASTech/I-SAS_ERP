<template>
    <div class="row" >
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('InvoiceSerialReport.InvoiceSerialReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);"> {{ $t('InvoiceSerialReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('InvoiceSerialReport.InvoiceSerialReport') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a  v-if="isValid('CanPrintAccountLedger')"  v-on:click="PrintRdlc()"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('AccountLedger.Print') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('InvoiceSerialReport.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           
           
            <div class="card">
                <div class="card-header">
                    <div class="row align-items-center">
                        <div class="col-lg-3">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="serialNo" type="text" class="form-control" @update:modelValue="GetInvoiceSerialReport"
                                    :placeholder=" $t('InvoiceSerialReport.SearchBySerialNumber') "
                                    aria-label="Example text with button addon"
                                    aria-describedby="button-addon1, button-addon2">
                                
                            </div>
                        </div>
                        <div class=" col-lg-3   form-group">
                            <label>{{ $t('InvoiceSerialReport.FromDate') }}</label>
                            <datepicker v-model="fromDate"  @update:modelValue="GetInvoiceSerialReport"/>
                        </div>
                        <div class=" col-lg-3   form-group">
                            <label>{{ $t('InvoiceSerialReport.ToDate') }}</label>
                            <datepicker v-model="toDate"  @update:modelValue="GetInvoiceSerialReport"/>
                        </div>
                        <div class=" col-lg-3 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>

                    </div>
                </div>
                <div class="card-body" >
                    <div>
                      
                        <div class="table-responsive">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>

                                        <th>
                                            {{ $t('InvoiceSerialReport.InvoiceNo') }}
                                        </th>
                                        <th>
                                            {{ $t('InvoiceSerialReport.Date') }}
                                        </th>
                                        <th>
                                            {{ $t('InvoiceSerialReport.CustomerNameEnglish') }}
                                        </th>
                                        <th>
                                            {{ $t('InvoiceSerialReport.CustomerNameArabic') }}
                                        </th>
                                        <th>
                                            {{ $t('InvoiceSerialReport.TotalAmount') }}
                                        </th>
                                       

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(invoice, i) in invoiceList" v-bind:key="i">
                                        <td>
                                            {{ i + 1 }}
                                        </td>
                                        <td>
                                            <strong>
                                                    <a href="javascript:void(0)" v-on:click="ViewInvoice(invoice.id)"> {{ invoice.invoiceNo }}</a>
                                                </strong> <br />
                                           
                                        </td>
                                        <td>
                                            {{ invoice.date }}
                                        </td>
                                        <td>
                                            {{ invoice.customerName }}
                                        </td>
                                        <td>
                                            {{ invoice.customerNameArabic }}
                                        </td>
                                        <td>
                                            {{ invoice.total }}
                                        </td>
                                        

                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    
                </div>


            </div>
        </div>
    </div>
   
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
export default {
    mixins: [clickMixin],
    props: ['formName'],
    data: function () {
        return {
            allowBranches: false,

            branchIds: [],
            branchId: '',
            // search: '',
            fromDate: '',
            toDate: '',
            serialNo: '',
            invoiceList: [],
        }
    },
    watch: {
        
        branchIds: function () {
            var root = this;
            this.branchId = '';
            this.branchIds.forEach(function (result) {
                root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
            })
            this.GetInvoiceSerialReport();
        }
    },
    created: function () {
        this.fromDate = moment().format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
    },
    
    methods: {
        ViewInvoice: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id , { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$router.push({
                                path: '/InvoiceView',
                                query: {
                                    data: response.data,
                                    fromReport:true
                                }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
        },
        PrintRdlc: function(){
                this.isShown = false;
                //todate = moment().add(1, 'days').format("DD MMM YYYY")
                this.$https.get('/Report/InvoiceSerialReport?fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&isLedger=true' + '&accountId=' + this.accountId + '&dateType=' + this.dateType , { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download', 'Report ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();
                    });
            },
        GetInvoiceSerialReport: function ( ) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.isShown = false;

            this.$https.get('/Report/GetInvoiceSerialReport?fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&serialNumber=' + this.serialNo + '&branchId=' + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {

                        root.invoiceList = response.data;
                    }
                }).catch(error => {

                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: 'Some thing went wrong!',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                });
        },

    },

    mounted: function () {
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

    }
}
</script>
