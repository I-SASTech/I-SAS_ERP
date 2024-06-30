<template>

    <div class="col-lg-12 col-sm-12 ml-auto mr-auto">


        <div class="card">
            <div class="card-header ">
                <h4 class="card-title DayHeading">{{ $t('InvocieDashboard.SaleInvoice') }}</h4>
                <div class="row">
                    <div class="col-md-5 col-lg-5">
                        <div class="form-group">
                            <datepicker v-model="search" :key="rander" />
                        </div>
                    </div>

                </div>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-lg-12">

                        <div class="tab-content" id="nav-tabContent">
                            <div v-if="active == 'Paid'">
                                <div>
                                    <div>
                                        <div class="mt-2">
                                            <div>
                                                <div class="col-lg-12">


                                                    <div class="mt-2">
                                                        <div>
                                                            <table class="table table-shopping">
                                                                <thead class="">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>
                                                                            {{ $t('InvocieDashboard.InvoiceNo') }}
                                                                        </th>
                                                                        <th>
                                                                            {{ $t('InvocieDashboard.Type') }}
                                                                        </th>
                                                                        <th>
                                                                            {{ $t('InvocieDashboard.Date') }}
                                                                        </th>
                                                                        <th>
                                                                            {{ $t('InvocieDashboard.CustomerName') }}
                                                                        </th>
                                                                        <th>
                                                                            {{ $t('InvocieDashboard.NetAmount') }}
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(sale, index) in saleList"
                                                                        v-bind:key="index">
                                                                        <td v-if="currentPage === 1">
                                                                            {{ index + 1 }}
                                                                        </td>
                                                                        <td v-else>
                                                                            {{ ((currentPage * 10) - 10) + (index + 1)
                                                                            }}
                                                                        </td>
                                                                        <td>

                                                                            {{ sale.registrationNumber }}


                                                                        </td>
                                                                        <td>
                                                                            {{ sale.isCredit ? 'Credit' : 'Cash' }}
                                                                        </td>
                                                                        <td>
                                                                            {{ sale.date }}
                                                                        </td>
                                                                        <td>
                                                                            {{ sale.customerName }}
                                                                        </td>
                                                                        <td>
                                                                            {{
                                                                                    parseFloat(sale.netAmount).toFixed(3).slice(0,
                                                                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                                            "$1,")
                                                                            }}
                                                                        </td>
                                                                        <td>


                                                                            <a href="javascript:void(0)"
                                                                                class="btn  btn-icon btn-primary btn-sm"
                                                                                v-on:click="goToPaymentDetail(sale.id)"><i
                                                                                    class=" fa fa-eye"></i></a>

                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="float-left">
                                                    <span v-if="currentPage === 1 && rowCount === 0"> {{
                                                            $t('Pagination.ShowingEntries')
                                                    }}</span>
                                                    <span v-else-if="currentPage === 1 && rowCount < 10"> {{
                                                            $t('Pagination.Showing')
                                                    }} {{ currentPage }} {{
        $t('Pagination.to')
}} {{ rowCount }} {{ $t('Pagination.of') }}
                                                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                                    <span v-else-if="currentPage === 1 && rowCount >= 11"> {{
                                                            $t('Pagination.Showing')
                                                    }} {{ currentPage }} {{
        $t('Pagination.to')
}} {{ currentPage * 10 }} {{ $t('Pagination.of')
}} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                                    <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }}
                                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10
                                                        }} of
                                                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{
                                                            $t('Pagination.Showing')
                                                    }} {{ (currentPage * 10) - 9 }} {{
        $t('Pagination.to')
}} {{ currentPage * 10 }} {{ $t('Pagination.of')
}} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                                    <span v-else-if="currentPage === pageCount"> {{
                                                            $t('Pagination.Showing')
                                                    }} {{ (currentPage * 10) - 9 }} {{
        $t('Pagination.to')
}} {{ rowCount }} {{ $t('Pagination.of') }}
                                                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                                </div>
                                                <div class="float-right">
                                                    <div class="" v-on:click="getPage()">
                                                        <b-pagination pills size="sm" v-model="currentPage"
                                                            :total-rows="rowCount" :per-page="10"
                                                            :first-text="$t('Table.First')"
                                                            :prev-text="$t('Table.Previous')"
                                                            :next-text="$t('Table.Next')"
                                                            :last-text="$t('Table.Last')"></b-pagination>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>





                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</template>

<script>
import moment from "moment";
export default {
    data: function () {
        return {
            active: 'Paid',
            rander: 0,
            search: '',
            saleList: [],
            purchasePostList: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '',



        }
    },
    watch: {
        search: function (val) {


            this.getData(val, 1, this.active);
        }
    },
    methods: {

        goToPaymentDetail: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Sale/SaleDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$router.push({
                            path: '/SalePaymentDetail',
                            query: { data: response.data, dashboard: true }
                        })
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },

        getPage: function () {
            this.getData(this.search, this.currentPage, this.active);
        },


        getData: function (search, currentPage, status) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Company/SaleList?status=' + status + '&search=' + search + '&pageNumber=' + currentPage, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    root.saleList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;

                });
        },

    },
    mounted: function () {
        this.getData(this.search, 1, 'Paid');
        this.rander++;
        this.search = moment().format("DD MMM YYYY");

        this.rander++;
    },
}
</script>