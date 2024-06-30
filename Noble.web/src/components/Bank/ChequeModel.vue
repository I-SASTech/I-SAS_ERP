<template>
    <modal :show="show" v-if=" isValid('CanAddCategory') || isValid('CanEditCategory') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('ChequeBook.ChequeBook') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-6 "
                         v-bind:class="{ 'has-danger': v$.chequeBook.bookNo.$error }">
                        <label class="text  font-weight-bolder">
                            {{ $t('ChequeBook.BookNo') }}:<span class="text-danger">
                                *
                            </span>
                        </label>
                        <input class="form-control" v-model="v$.chequeBook.bookNo.$model" type="text" />
                    </div>

                    <div class="form-group has-label col-sm-6 "
                         v-bind:class="{ 'has-danger': v$.chequeBook.noOfCheques.$error }">
                        <label class="text  font-weight-bolder">
                            {{ $t('ChequeBook.NoOfCheques') }}:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" @update:modelValue="LastNo" v-bind:disabled="type"
                               v-model="v$.chequeBook.noOfCheques.$model" type="number" />
                    </div>
                    <div class="form-group has-label col-sm-6 "
                         v-bind:class="{ 'has-danger': v$.chequeBook.startingNo.$error }">
                        <label class="text  font-weight-bolder">
                            {{ $t('ChequeBook.StartingNo') }}:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-bind:disabled="type" @update:modelValue="LastNo"
                               v-model="v$.chequeBook.startingNo.$model" type="number" />
                    </div>

                    <div class="form-group has-label col-sm-6 "
                         v-bind:class="{ 'has-danger': v$.chequeBook.lastNo.$error }">
                        <label class="text  font-weight-bolder">
                            {{ $t('ChequeBook.LastNo') }}:<span class="text-danger">
                                *
                            </span>
                        </label>
                        <input class="form-control" disabled v-model="v$.chequeBook.lastNo.$model" type="number" />
                    </div>

                    <div class="card" v-if="chequeBook.id != '00000000-0000-0000-0000-000000000000'">

                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.BookNo') }}
                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.SerialNo') }}
                                            </th>
                                            <th class="text-center ">
                                                {{ $t('ChequeBook.ChequeNo') }}
                                            </th>

                                            <th class="text-center ">
                                                {{ $t('ChequeBook.Status') }}
                                            </th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(region,index) in chequeBook.chequeBookItems" v-bind:key="region.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>
                                            <td class="text-center ">
                                                {{ region.bookNo }}

                                            </td>
                                            <td class="text-center ">
                                                {{ region.serialNo }}

                                            </td>
                                            <td class="text-center ">
                                                {{ region.chequeNo }}

                                            </td>

                                            <td>
                                                <span v-if="region.isActive"
                                                      class="badge badge-boxed  badge-outline-success">
                                                    {{
                                                            $t('Active')
                                                    }}
                                                </span>
                                                <span v-else class="badge badge-boxed  badge-outline-danger">
                                                    {{
                                                        $t('De-Active')
                                                    }}
                                                </span>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveLoanRecovery"
                        v-bind:disabled="v$.chequeBook.$invalid">
                    {{ $t('ChequeBook.Save') }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close(false)">
                    {{
                        $t('ChequeBook.Cancel')
                    }}
                </button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        props: ['show', 'chequebook', 'loanDetail', 'type'],
        components: {
           Loading 
        },
             setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {

                currency: '',
                arabic: '',
                english: '',
                render: 0,
                dateRender: 0,
                loading: false,
                chequeBook: {},
            }
        },
        validations() {
        return{
                chequeBook: {
                bookNo: {
                    required
                },



                noOfCheques: {
                    required

                },
                startingNo: {
                    required

                },
                lastNo: {
                    required

                },

            }
        }
        },
        methods: {

            LastNo: function () {


                this.chequeBook.lastNo = parseInt(this.chequeBook.startingNo) + parseInt(this.chequeBook.noOfCheques);

                this.chequeBook.lastNo = this.chequeBook.lastNo - 1;
                //this.chequeBook.lastNo = j + this.chequeBook.lastNo;


            },
            close: function (x) {

                if (x == true)
                    this.$emit('close', x);
                else {
                    this.$emit('close', false);
                }

            },
            SaveLoanRecovery: function () {

                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.chequeBook.lastNo <= this.chequeBook.startingNo) {
                    root.$swal({
                        title: this.$t('ChequeBook.Error'),
                        text: this.$t('ChequeBook.lessThanOrEqualTo'),
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                    return;
                }

                this.chequeBook.remaining = this.chequeBook.noOfCheques;

                this.$https.post('/Payroll/SaveChequeBookInformation', this.chequeBook, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.isSuccess == true) {
                            if (root.type != "Edit") {

                                root.$swal({
                                    title: root.$t('ChequeBook.Saved'),
                                    text: root.$t('ChequeBook.SavedSuccessfully'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });

                                root.close(true);
                            }
                            else {

                                root.$swal({
                                    title: root.$t('ChequeBook.Update'),
                                    text: root.$t('ChequeBook.UpdateSuccessfully'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close(true);

                            }
                        }
                        else {
                            root.$swal({
                                title: this.$t('ChequeBook.Error'),
                                text: this.$t('ChequeBook.YourLoanRecoveryNameAlreadyExist'),
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: this.$t('ChequeBook.SomethingWrong'),
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            }
        },
        created: function () {
            this.chequeBook = this.chequebook;

        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');
            this.chequeBook.date = moment().format('LLL');
        }

    }
</script>
<style scoped>

    table {
        width: 100%;
    }

    thead, tbody tr {
        display: table;
        width: 100%;
        table-layout: fixed;
    }

    tbody {
        display: block;
        overflow-y: auto;
        table-layout: fixed;
        max-height: 600px;
    }

    ::-webkit-scrollbar {
        width: 11px !important;
        height: 10px !important;
    }
</style>


