<template>
    <div class="row" v-if="isValid('CanViewVatPayableReport')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('VatPayableReport.VatPayableReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('VatPayableReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('VatPayableReport.VatPayableReport') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc(fromDate,toDate,true)"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('Print') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row align-items-center">

                <div class=" col-lg-3   form-group">
                    <label>{{ $t('VatPayableReport.FromDate') }}</label>
                    <datepicker v-model="fromDate" :key="rander" />
                </div>

                <div class=" col-lg-3   form-group">
                    <label>{{ $t('VatPayableReport.ToDate') }}</label>
                    <datepicker v-model="toDate" :key="rander" />
                </div>
                <div class=" col-lg-3 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
            </div>
            <div>
                <iframe :key="changereport" height="1100" width="100%" :src="reportsrc"></iframe>
            </div>

        </div>
        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt" @close="show=false" @IsSave="IsSave" />

    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'


    import moment from "moment";
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
            allowBranches: false,
                branchIds: [],
                branchId: '',
                reportsrc: '',
                changereport: 0,
                reportsrc1: '',
                changereportt: 0,
                show: false,
                vatPaidDetail: '',
                date: '',
                fromDate: '',
                toDate: '',
                rander: 0,
                printRenderPdf: 0,
                printRender: 0,
                paidaccounts: [],
                payableaccounts: [],
                advanceFilters: false,
                isShown: false,
                isDownload: false,
                combineDate: '',
                language: 'Nothing',
                totalvatpaid: 0,
                totalvatpayable: 0,
                VatPayable: 0,
                printDetails: [],
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },

            }
        },
        watch: {
            toDate: function (todate) {
                var fromdate = this.fromDate;

                this.PrintRdlc(fromdate, todate, false);
            },
            fromDate: function (fromdate) {
                var todate = this.toDate;


                this.PrintRdlc(fromdate, todate, false);

            },
            branchIds: function () {
                var root = this;
                this.branchId = '';
                this.branchIds.forEach(function (result) {
                    root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
                })
                this.PrintRdlc(this.fromDate, this.toDate, false);
            }
        },
        methods: {
            getBase64Image: function (path, pdf) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';


                if (pdf) {
                    root.$https
                        .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                            if (response.data != null) {
                                root.filePath = response.data;
                                root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                            }
                        }).then(function () {
                            root.printDetails.push({
                                fromDate: root.fromDate,
                                toDate: root.toDate,
                                paidaccounts: root.paidaccounts,
                                payableaccounts: root.payableaccounts,
                                totalvatpaid: root.totalvatpaid,
                                totalvatpayable: root.totalvatpayable,
                                VatPayable: root.VatPayable,
                            })

                            root.isDownload = true;
                            root.printRenderPdf++;
                        });
                }
                else {
                    root.$https
                        .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                            if (response.data != null) {
                                root.filePath = response.data;
                                root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                            }
                        }).then(function () {
                            root.printDetails.push({
                                fromDate: root.fromDate,
                                toDate: root.toDate,
                                paidaccounts: root.paidaccounts,
                                payableaccounts: root.payableaccounts,
                                totalvatpaid: root.totalvatpaid,
                                totalvatpayable: root.totalvatpayable,
                                VatPayable: root.VatPayable,
                            })

                            root.isShown = true;

                            root.printRender++;
                        });
                }

            },
            GetHeaderDetail: function (pdf) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            root.GetInvoicePrintSetting();
                            root.getBase64Image(root.headerFooter.company.logoPath, pdf);
                        }
                    });
            },
            GetInvoicePrintSetting: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/printSettingDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.headerFooter.footerEn = response.data.termsInEng;
                            root.headerFooter.footerAr = response.data.termsInAr;
                            root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                            root.headerFooter.englishName = response.data.englishName;
                            root.headerFooter.arabicName = response.data.arabicName;
                            root.headerFooter.customerAddress = response.data.customerAddress;
                            root.headerFooter.customerVat = response.data.customerVat;
                            root.headerFooter.customerNumber = response.data.customerNumber;
                            root.headerFooter.cargoName = response.data.cargoName;
                            root.headerFooter.customerTelephone = response.data.customerTelephone;
                            root.headerFooter.itemPieceSize = response.data.itemPieceSize;
                            root.headerFooter.styleNo = response.data.styleNo;
                            root.headerFooter.blindPrint = response.data.blindPrint;
                            root.headerFooter.showBankAccount = response.data.showBankAccount;
                            root.headerFooter.bankAccount1 = response.data.bankAccount1;
                            root.headerFooter.bankAccount2 = response.data.bankAccount2;
                            root.headerFooter.bankIcon1 = response.data.bankIcon1;
                            root.headerFooter.bankIcon2 = response.data.bankIcon2;
                            root.headerFooter.customerNameEn = response.data.customerNameEn;
                            root.headerFooter.customerNameAr = response.data.customerNameAr;

                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },

            PrintDetails: function (download) {

                this.GetHeaderDetail(download);

            },
            languageChange: function (lan) {
                if (this.language == lan) {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/VatPayableReport');

                }
            },
            convertDate: function (date) {
                return moment(date).format('l');
            },
            getPage: function () {

                this.PrintRdlc(this.fromDate, this.toDate, false);
            },

            GetInventoryList: function (fromdate, todate) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                //todate=    moment().add(1, 'days').format("DD MMM YYYY")
                this.$https.get('/Report/VatPayable?fromDate=' + fromdate + '&toDate=' + todate, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.vatPaidDetail = response.data;
                            root.paidaccounts = response.data.vatPaid;
                            root.totalvatpaid = Math.abs(root.paidaccounts.reduce(function (prev, item) {
                                return prev + Number(item.amount);

                            }, 0));
                            root.payableaccounts = response.data.vatPayable;
                            root.totalvatpayable = Math.abs(root.payableaccounts.reduce(function (prev, item) {
                                return prev + Number(item.amount);

                            }, 0));
                            root.VatPayable = root.totalvatpayable - root.totalvatpaid;
                            root.VatPayable = root.VatPayable < 0 ? root.VatPayable * -1 : root.VatPayable;
                        }
                    });


            },
            IsSave: function () {
                this.showReport = !this.showReport;
            },
            PrintRdlc: function (fromdate, todate, val) {

                var companyId = '';
                companyId = localStorage.getItem('CompanyID');
                

                if (val) {
                    this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/VatReports/VatReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&formName=VatPayableReport' + "&Print=" + val + "&branchId=" +this.branchId
                    this.changereportt++;
                    this.show = !this.show;
                }
                else {
                    this.reportsrc = this.$ReportServer + '/ReportManagementModule/VatReports/VatReports.aspx?companyId=' + companyId + '&fromDate=' + fromdate + '&toDate=' + todate + '&formName=VatPayableReport' + "&Print=" + val + "&branchId=" + this.branchId
                    this.changereport++;
                }
            },
            PrintCsv: function () {
                var root = this;
                root.$https.post('/Report/VatPayableCsv?language=' + this.$i18n.locale + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&companyId=' + localStorage.getItem('CompanyID'), root.vatPaidDetail, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }, responseType: 'blob' })
                    .then(function (response) {

                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'VatPayableReport.csv');
                        document.body.appendChild(link);
                        link.click();

                    });
            },
        },
        mounted: function () {
            this.language = this.$i18n.locale;
            this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.rander++;
            this.PrintRdlc(this.fromDate, this.toDate, false);
            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
        }
    }
</script>