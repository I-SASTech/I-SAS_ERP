<template>
    <div>

        <div id="waiting"
            style="display: none; position: fixed; cursor: wait; height: 100vh; width: 100vw; top: 0; left: 0; background: black; opacity: 30%; z-index: 2">

        </div>


        <nav style="display: flex;
    justify-content: space-between;
    gap: .5rem; position: fixed;
    width: calc(100% - 32px); left:0;
    top: 0;
    padding: 8px 16px;
    background-color: white;
    z-index: 1;
    box-shadow: rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0.1) 0px 4px 6px -1px, rgba(0, 0, 0, 0.1) 0px 2px 4px -2px;
 ">
            <div>
                <div class="logowrapper" style="height: 40px">
                    <a href="">
                        <img 
                            alt="Logo" class="h-10 w-full object-contain">
                    </a>
                </div>
            </div>
            <div style="display: flex; gap: 1rem">
                <button onclick="window.print()" id="printbutton" class="button"><svg xmlns="http://www.w3.org/2000/svg" fill="none"
                        viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" aria-hidden="true">
                        <path stroke-linecap="round" stroke-linejoin="round"
                            d="M17 17h2a2 2 0 002-2v-4a2 2 0 00-2-2H5a2 2 0 00-2 2v4a2 2 0 002 2h2m2 4h6a2 2 0 002-2v-4a2 2 0 00-2-2H9a2 2 0 00-2 2v4a2 2 0 002 2zm8-12V5a2 2 0 00-2-2H9a2 2 0 00-2 2v4h10z" />
                    </svg> <span>Print</span></button>

              

              
            </div>

        </nav>
        <div style="height: 20px"></div>
        <div style="padding: 4rem; background-color: white; display: flex; justify-content: center">


            <div
                style="background-color: white; display: inline-block; box-shadow: rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0.25) 0px 25px 50px -12px;">
                <main>
                    <div class="box box-border" id="printable">


                        <table style="width: 100%">
                            <thead>
                                <tr>
                                    <th>
                                        <div class="" style=" position:relative;  overflow: hidden !important;">

                                            <table width='100%'>
                                                <tr>
                                                    <td class="title">

                                                        <div style="height: 100px;width: 100%; position: relative;">

                                                            <img style="position: absolute; left: 0; top: 50%; transform: translateY(-50%)"
                                                                class="logo"
                                                                src="https://invoice.techplus.dev/storage/software-setting/logo-ky9R4qyAr7TU9JtC5ofhe2wwZKDIRC0pnZZkXCgf.png"
                                                                height=50%>


                                                        </div>

                                                    </td>

                                                    <td width="30%" style="height: 145px; overflow: hidden">

                                                        <table class="float-right icon-info-table ">
                                                            <tbody>
                                                                <tr>

                                                                    <td class="text-right icon">
                                                                        <img
                                                                            src="data:image/svg+xml;base64,CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiBmaWxsPSJub25lIiB2aWV3Qm94PSIwIDAgMjQgMjQiIHN0cm9rZS13aWR0aD0iMS41IiBzdHJva2U9ImdyZXkiIGNsYXNzPSJ3LTYgaC02Ij4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8cGF0aCBzdHJva2UtbGluZWNhcD0icm91bmQiIHN0cm9rZS1saW5lam9pbj0icm91bmQiIGQ9Ik0xMC41IDEuNUg4LjI1QTIuMjUgMi4yNSAwIDAwNiAzLjc1djE2LjVhMi4yNSAyLjI1IDAgMDAyLjI1IDIuMjVoNy41QTIuMjUgMi4yNSAwIDAwMTggMjAuMjVWMy43NWEyLjI1IDIuMjUgMCAwMC0yLjI1LTIuMjVIMTMuNW0tMyAwVjNoM1YxLjVtLTMgMGgzbS0zIDE4Ljc1aDMiIC8+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwvc3ZnPg==" />
                                                                    </td>
                                                                    <td class="info text-left">534543534</td>
                                                                </tr>


                                                                <tr>

                                                                    <td class="text-right icon">
                                                                        <img
                                                                            src="data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIGZpbGw9Im5vbmUiIHZpZXdCb3g9IjAgMCAyNCAyNCIgc3Ryb2tlLXdpZHRoPSIxLjUiIHN0cm9rZT0iZ3JleSIgY2xhc3M9InctNiBoLTYiPgo8cGF0aCBzdHJva2UtbGluZWNhcD0icm91bmQiIHN0cm9rZS1saW5lam9pbj0icm91bmQiIGQ9Ik0yMS43NSA5di45MDZhMi4yNSAyLjI1IDAgMDEtMS4xODMgMS45ODFsLTYuNDc4IDMuNDg4TTIuMjUgOXYuOTA2YTIuMjUgMi4yNSAwIDAwMS4xODMgMS45ODFsNi40NzggMy40ODhtOC44MzkgMi41MWwtNC42Ni0yLjUxbTAgMGwtMS4wMjMtLjU1YTIuMjUgMi4yNSAwIDAwLTIuMTM0IDBsLTEuMDIyLjU1bTAgMGwtNC42NjEgMi41MW0xNi41IDEuNjE1YTIuMjUgMi4yNSAwIDAxLTIuMjUgMi4yNWgtMTVhMi4yNSAyLjI1IDAgMDEtMi4yNS0yLjI1VjguODQ0YTIuMjUgMi4yNSAwIDAxMS4xODMtMS45OGw3LjUtNC4wNGEyLjI1IDIuMjUgMCAwMTIuMTM0IDBsNy41IDQuMDRhMi4yNSAyLjI1IDAgMDExLjE4MyAxLjk4VjE5LjV6IiAvPgo8L3N2Zz4K" />
                                                                    </td>
                                                                    <td class="info text-left">info@.com</td>
                                                                </tr>


                                                                <tr>

                                                                    <td class="text-right icon">
                                                                        <img
                                                                            src="data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIGZpbGw9Im5vbmUiIHZpZXdCb3g9IjAgMCAyNCAyNCIgc3Ryb2tlLXdpZHRoPSIxLjUiIHN0cm9rZT0iZ3JleSIgY2xhc3M9InctNiBoLTYiPgo8cGF0aCBzdHJva2UtbGluZWNhcD0icm91bmQiIHN0cm9rZS1saW5lam9pbj0icm91bmQiIGQ9Ik05IDYuNzVWMTVtNi02djguMjVtLjUwMyAzLjQ5OGw0Ljg3NS0yLjQzN2MuMzgxLS4xOS42MjItLjU4LjYyMi0xLjAwNlY0LjgyYzAtLjgzNi0uODgtMS4zOC0xLjYyOC0xLjAwNmwtMy44NjkgMS45MzRjLS4zMTcuMTU5LS42OS4xNTktMS4wMDYgMEw5LjUwMyAzLjI1MmExLjEyNSAxLjEyNSAwIDAwLTEuMDA2IDBMMy42MjIgNS42ODlDMy4yNCA1Ljg4IDMgNi4yNyAzIDYuNjk1VjE5LjE4YzAgLjgzNi44OCAxLjM4IDEuNjI4IDEuMDA2bDMuODY5LTEuOTM0Yy4zMTctLjE1OS42OS0uMTU5IDEuMDA2IDBsNC45OTQgMi40OTdjLjMxNy4xNTguNjkuMTU4IDEuMDA2IDB6IiAvPgo8L3N2Zz4K" />
                                                                    </td>
                                                                    <td class="info text-left">17 Ali Brothers,
                                                                        345,453</td>
                                                                </tr>


                                                            </tbody>
                                                        </table>

                                                    </td>
                                                </tr>
                                            </table>
                                            <hr style="border-color: red">
                                        </div>


                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <div>



                                            <div style="padding-top: 16px;">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td width="50%" class="vertical-top">
                                                            <table width='100%' class="icon-info-table">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Bill To:</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="info"
                                                                            style="font-weight: bold; font-size: 16px; padding-left: 12px">
                                                                            Zafar</td>
                                                                    </tr>






                                                                </tbody>
                                                            </table>
                                                        </td>

                                                        <td width="50%" class="vertical-top">
                                                            <table class="float-right icon-info-table">
                                                                <tbody>
                                                                    <tr>

                                                                        <td class="info"
                                                                            style="font-weight: bold; font-size: 16px; width: 200px; text-align: right">

                                                                            <span>Invoice No : </span>
                                                                        </td>
                                                                        <td class="info"> 2023-12-04-004</td>
                                                                    </tr>


                                                                    <tr>
                                                                        <td class="info"
                                                                            style="font-weight: bold; font-size: 16px; width: 200px; text-align: right">

                                                                            <span>Issue Date : </span>
                                                                        </td>
                                                                        <td class="info"> 2023-12-04</td>
                                                                    </tr>


                                                                    <tr>

                                                                        <td class="info"
                                                                            style="font-weight: bold; font-size: 16px; width: 200px; text-align: right">

                                                                            <span>Amount : </span>
                                                                        </td>
                                                                        <td class="info">  134.00</td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>







                                            <div class="invoice-box" style="padding-top: 16px">
                                                <table cellpadding="0" cellspacing="0" clas="invoice-services">
                                                    <tr class="heading">
                                                        <td class="text-left" style="width: 210px">
                                                            Passenger
                                                        </td>
                                                        <td class="text-center">Ticket</td>

                                                        <td class="text-center">
                                                            Ticket
                                                        </td>


                                                        <td class="text-center">
                                                            Remark
                                                        </td>

                                                        <td class="text-center" style="width: 80px">Price</td>
                                                        <td class="text-center" style="width: 80px">Quantity</td>
                                                        <td class="text-right" style="width: 80px">Total</td>
                                                    </tr>




                                                    <tr class="item invoice-item first">
                                                        <td>Zafar</td>

                                                        <td>23</td>


                                                        <td class="text-center"></td>

                                                        <td class="text-left">
                                                            <pre>
                                                        <p>Remark 1<br>Remark2<br>Remarks3</p>
                                                    </pre>
                                                        </td>

                                                        <td class="text-center">78</td>
                                                        <td class="text-center">1</td>
                                                        <td class="text-right">
                                                            ৳ 78
                                                        </td>

                                                    </tr>
                                                    <tr class="item invoice-item last with_top">
                                                        <td>Ahsan</td>

                                                        <td>24</td>


                                                        <td class="text-center"></td>

                                                        <td class="text-left">
                                                            <pre>
                                                        <p>Remark 1<br>Remark2<br>Remarks3</p>
                                                    </pre>
                                                        </td>

                                                        <td class="text-center">56</td>
                                                        <td class="text-center">1</td>
                                                        <td class="text-right">
                                                            ৳ 56
                                                        </td>

                                                    </tr>



                                                </table>



                                            </div>
                                            <div>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="60%"></td>
                                                        <td>
                                                            <table class="transaction-summary" width="100%">
                                                                <tr>
                                                                    <td class="font-bold">Net
                                                                        Payable:
                                                                    </td>
                                                                    <td class="text-right font-bold">
                                                                        ৳ 134.00
                                                                    </td>
                                                                </tr>


                                                                <tr>
                                                                    <td class="font-bold"
                                                                        style="border-top: 1px solid black;">Paid:</td>
                                                                    <td class="text-right font-bold"
                                                                        style="border-top: 1px solid black;">
                                                                         0.00
                                                                    </td>
                                                                </tr>


                                                                <tr>
                                                                    <td class="font-bold"
                                                                        style="border-top: 1px solid black;">Due:</td>
                                                                    <td class="text-right font-bold"
                                                                        style="border-top: 1px solid black;">
                                                                         134.00
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="font-bold"
                                                                        style="border-top: 1px solid black;">Status:</td>
                                                                    <td class="text-right font-bold"
                                                                        style="border-top: 1px solid black;">
                                                                        Due
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>




                                            <table style="width: 100%">
                                                <tr style=" height: auto">
                                                    <td style="width: 50%">
                                                        <div style="padding-top: 16px">
                                                            <h4 style="margin: 0; margin-bottom: 12px;">Bank Details</h4>
                                                            <table style="width: 100%;" class="bank-table">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Bank: </td>
                                                                        <td>sdaf Bank</td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Account Name: </td>
                                                                        <td>sdff</td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Account Number: </td>
                                                                        <td>6309205068600001</td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Short Code/Branch: </td>
                                                                        <td>67657</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </td>

                                                    <td
                                                        style="width: 50%; vertical-align: bottom; text-align: right; padding-left: 120px">



                                                    </td>

                                                </tr>
                                            </table>






                                        </div>

                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </tfoot>
                        </table>

                    </div>
                </main>
            </div>


        </div>




    </div>
</template>

<script>
import moment from "moment";

export default {

    data: function () {
        return {
            b2b: false,
            b2c: false,

        }
    },

    computed: {
        calulateTotalQty: function () {
            return this.list.saleItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0)
        },

    },
    methods: {

        getDate: function (x) {
            if (x == null || x == undefined)
                return '';
            else
                return moment(x).format('l');
        },





    },
    created() {

    },
    mounted: function () {



    },

}
</script>


<style scoped>

@media print {
  nav {
    display: none;
  }

  #waiting {
    display: none;
  }
  .print-only {
    display: block;

  /* Add any other elements you want to hide during printing */
}
}
@media screen {
  .print-only {
    display: none;
  }
}
.logowrapper img {
    height: 100%;
}

@font-face {
    font-family: 'Poppins';
    src: url("https://invoice.techplus.dev/fonts/Poppins-Light.ttf") format('truetype');
    font-weight: normal;
    font-style: normal;
}

* {
    font-family: 'Poppins', sans-serif;
    line-height: unset
}

.box {
    margin: auto;
    padding: 12px;
    font-size: 16px;
    color: #555;
}

.invoice-box table {
    width: 100%;
    line-height: inherit;
    text-align: left;
}

.invoice-box table td {
    padding: 5px;
    vertical-align: top;
}


.invoice-box table tr.top table td {
    padding-bottom: 20px;
}

.invoice-box table tr.top table td.title {
    font-size: 45px;
    line-height: 45px;
    color: #333;
}

.invoice-box table tr.information table td {
    padding-bottom: 40px;
}


.invoice-box table tr.details td {
    padding-bottom: 20px;
}





td.all-remark p,
td.all-remark h2,
td.all-remark h3 {
    margin: 2px 0;
}

td.all-remark blockquote {
    border-left: 4px solid #d8dbe1;
    padding-left: 4px;
    margin: 4px 2px;
}

td.all-remark pre {
    background: white;
    padding: 8px 12px;
    border-radius: 10px;
}


table.transaction-summary {
    border-collapse: collapse
}

.invoice-box table tr.heading td {
    background: #eee;
    font-weight: bold;
}

.bank-table,
.invoice-box table {
    width: 100%;
    border-collapse: collapse;
}

.bank-table td,
.invoice-box table td {
    border: 1px solid;
    border-collapse: collapse;
}




.bank-table td {
    padding: 4px 8px;
}

table.transaction-summary tr td {
    padding-right: 0 !important;
    padding-left: 0 !important;
}

.text-right {
    text-align: right;
}

.text-left {
    text-align: left;
}

.text-center {
    text-align: center;
}

.float-right {
    float: right;
}

.vertical-top {
    vertical-align: top
}

.font-bold {
    font-weight: bold;
}


.icon-info-table tr td:first-child.info {
    padding-right: 4px;
}

.icon-info-table tr td:last-child {
    padding-left: 4px;
}

.icon-info-table tr td.icon {
    width: 20px;
    vertical-align: top
}

.icon-info-table tr td.info {
    font-size: 14px;
}

.page-break {
    page-break-after: always;
}

.button {
    gap: .5rem;
    display: inline-flex;
    justify-content: center;
    align-items: center;
    background: transparent;
    border: none;
    color: #306eb6;
    padding: 8px 0px;
    text-align: center;
    text-decoration: none;
    font-size: 16px;
    border-bottom: 1px solid #306eb6;
}

.button svg {
    height: 20px;
}

thead th {
    height: 150px;
}

tfoot td {
    height: 30px;
}

.report-footer {
    position: fixed;
    bottom: 0px;
    height: 30px;
    display: block;
    width: 100%;
    text-align: center;
    /*border-top: solid 1px #ccc;*/
    overflow: hidden;
}

* {
    overflow: visible !important;
}

.box {
    width: 1000px;
}

.logo {
    max-height: 100px;
    object-fit: contain;
    object-position: top left;
}

.icon-info-table tr td.icon img {
    width: 15px;
    height: 15px;
    /* position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%,-50%) */
}</style>