<template>
    <modal :show="show" :modalLarge="true" v-if=" isValid('CanAddCategory') || isValid('CanEditCategory') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('ChequeBook.ChequeBook') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="row">
                                <div class="col">
                                </div>
                                <div class="col-auto align-self-center">
                                    <a v-on:click="openmodel" href="javascript:void(0);"
                                        class="btn btn-sm btn-outline-primary mx-1">
                                        <i class="align-self-center icon-xs ti-plus"></i>
                                        {{ $t('ChequeBook.AddChequeBook') }}
                                    </a>
                                   
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th class="text-center ">
                                        {{ $t('ChequeBook.BookNo') }}
                                    </th>
                                    <th class="text-center ">
                                        {{ $t('ChequeBook.RegDate') }}

                                    </th>
                                    <th class="text-center ">
                                        {{ $t('ChequeBook.NoOfCheques') }}

                                    </th>
                                    <th class="text-center ">
                                        {{ $t('ChequeBook.StartingNo') }}
                                    </th>
                                    <th class="text-center ">
                                        {{ $t('ChequeBook.LastNo') }}
                                    </th>
                                    <th class="text-center ">
                                        {{ $t('ChequeBook.Used') }}

                                    </th>
                                    <th class="text-center ">
                                        {{ $t('ChequeBook.Remaining') }}

                                    </th>

                                    <th class="text-center ">
                                        {{ $t('ChequeBook.Blocked') }}

                                    </th>
                                    <th class="text-center ">
                                        {{ $t('ChequeBook.Status') }}

                                    </th>
                                    <th class="text-center " v-if="isValid('CanBlockCheque')">
                                        {{ $t('ChequeBook.Action') }}

                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(region, index) in chequeList" v-bind:key="region.id">
                                    <td>
                                        {{ index + 1 }}
                                    </td>

                                    <td class="text-center ">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditRegion(region.id)">
                                                {{ region.bookNo }}</a>
                                        </strong>
                                    </td>

                                    <td class="text-center ">
                                        {{ region.dates }}
                                    </td>
                                    <td class="text-center ">
                                        {{ region.noOfCheques }}
                                    </td>
                                    <td class="text-center ">
                                        {{ region.startingNo }}
                                    </td>
                                    <td class="text-center ">
                                        {{ region.lastNo }}
                                    </td>
                                    <td class="text-center ">
                                        {{ region.usedCheck }}
                                    </td>
                                    <td class="text-center ">
                                        {{ (region.remaining - region.usedCheck) - region.blocked }}
                                    </td>
                                    <td class="text-center ">
                                        {{ region.blocked }}
                                    </td>

                                    <td class="text-center ">
                                        <span v-if="region.remaining == 0"
                                            class="badge badge-boxed  badge-outline-success">
                                            Used
                                        </span>
                                        <span v-else-if="region.remaining - region.blocked == 0"
                                            class="badge badge-boxed  badge-outline-success">
                                            Used
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-success">
                                            InUsed
                                        </span>
                                    </td>


                                    <td v-if="region.isBlock && isValid('CanBlockCheque')" class="  text-center "
                                        style="color:red">{{ $t('ChequeBook.Blocked') }}
                                    </td>
                                    <td v-else-if="(region.remaining - region.usedCheck == 0) && isValid('CanBlockCheque')"
                                        class="  text-center ">Used</td>
                                    <td class="text-center" v-else-if="!region.isBlock && isValid('CanBlockCheque')"> <a
                                            href="javascript:void(0)" class="btn btn-soft-primary btn-sm"
                                            v-on:click="BlockModel(region.id)">{{ $t('ChequeBook.Blocked')
                                            }}</a></td>




                                </tr>

                            </tbody>
                        </table>
                    </div>




                    <chequemodel :chequebook="newChequeBook" :show="chequeBookShow" :type="type" v-if="chequeBookShow"
                        @close="RefreshList" />
                    <blockmodel :id="id" :show="blockShow" v-if="blockShow" @close="RefreshListblock" />
                </div>
            </div>
            <div class="modal-footer">
               
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{
                        $t('ChequeBook.Close')
                }}</button>
            </div>

        </div>



    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'

export default {
    mixins: [clickMixin],
    props: ['show', 'loanDetail', 'bankId'],

    data: function () {
        return {
            id: '',
            type: false,

            currency: '',
            arabic: '',
            english: '',
            render: 0,
            randerList: 0,
            chequeList: [],
            loading: false,
            chequeBookShow: false,
            blockShow: false,
            newChequeBook: {
                id: '00000000-0000-0000-0000-000000000000',
                bankNo: '',
                bankId: '',
                bookNo: '',
                noOfCheques: '',
                startingNo: '',
                lastNo: '',
                used: 0,
                remaining: 0,
                reason: '',
                date: '',
                isActive: true,
            },
        }
    },
    computed: {

        resultQuery: function () {
            var root = this;
            if (this.searchQuery) {
                return root.chequeList.filter((region) => {

                    return root.searchQuery.toLowerCase().split(' ').every(v => region.area.toLowerCase().includes(v) || region.stateId.toLowerCase().includes(v) || region.cityName.toLowerCase().includes(v) || region.code.toLowerCase().includes(v) || region.description.toLowerCase().includes(v))
                })
            } else {
                return root.chequeList;
            }
        },






    },

    methods: {
        EditRegion: function (Id) {


            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Payroll/ChequeBookDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {

                        root.newChequeBook.id = response.data.id;
                        root.newChequeBook.bankNo = response.data.bankNo;
                        root.newChequeBook.bankId = response.data.bankId;
                        root.newChequeBook.bookNo = response.data.bookNo;
                        root.newChequeBook.noOfCheques = response.data.noOfCheques;
                        root.newChequeBook.startingNo = response.data.startingNo;
                        root.newChequeBook.lastNo = response.data.lastNo;
                        root.newChequeBook.used = response.data.used;
                        root.newChequeBook.remaining = response.data.remaining;
                        root.newChequeBook.reason = response.data.reason;
                        root.newChequeBook.date = response.data.date;
                        root.newChequeBook.isActive = response.data.isActive;
                        root.newChequeBook.isActive = response.data.isActive;
                        root.newChequeBook.chequeBookItems = response.data.chequeBookItems;
                        root.type = true

                        root.chequeBookShow = !root.chequeBookShow;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });

        },
        openmodel: function () {
            this.newChequeBook = {
                id: '00000000-0000-0000-0000-000000000000',
                bankId: this.bankId,
                bankNo: '',
                bookNo: '',
                noOfCheques: '',
                startingNo: '',
                lastNo: '',
                used: 0,
                remaining: 0,
                reason: '',
                date: '',
                isActive: true,

            }
            this.chequeBookShow = !this.chequeBookShow;
        },
        BlockModel: function (id) {
            var root = this;
            this.$swal({
                title: this.$t('ChequeBook.AreYouSure'),
                text: this.$t('ChequeBook.BlockChequeBook'),
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: this.$t('ChequeBook.YesDeleteIt'),
                closeOnConfirm: false,
                closeOnCancel: true
            }).then(function (result) {
                if (result.isConfirmed) {

                    root.id = id;
                    root.blockShow = !root.blockShow;
                }
                else {
                    this.$swal(this.$t('ChequeBook.Cancelled'), this.$t('ChequeBook.YourFileIntact'), this.$t('ChequeBook.info'));
                }
            });

        },
        GetChequeData: function () {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Payroll/ChequeBookList?isDropdown=false' + '&id=' + this.bankId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.chequeList = response.data.results;
                }
            });
        },
        RefreshListblock: function (x) {

            if (x == true) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Payroll/ChequeBookList?isDropdown=false' + '&id=' + this.bankId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.chequeList = response.data.results;
                        root.blockShow = false;
                        root.randerList++;
                    }
                });
            }
            else {
                this.blockShow = false;

            }
        },
        RefreshList: function (x) {

            if (x == true) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Payroll/ChequeBookList?isDropdown=false' + '&id=' + this.bankId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.chequeList = response.data.results;
                        root.chequeBookShow = false;
                        root.randerList++;
                    }
                });
            }
            else {
                this.chequeBookShow = false;

            }
        },



        close: function () {
            this.$emit('close');
        },

    },
    mounted: function () {
        this.GetChequeData();
        this.currency = localStorage.getItem('currency');

    }
}
</script>

<style scoped>
.Heading1 {
    font-size: 25px !important;
    font-style: normal;
    font-weight: 600;
    color: #3178F6;
}

.Heading2 {
    font-size: 18px !important;
    font-style: normal;
    color: black;
}
</style>
