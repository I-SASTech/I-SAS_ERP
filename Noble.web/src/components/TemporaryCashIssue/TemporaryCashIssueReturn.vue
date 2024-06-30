<template>
    <modal :show="show" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-if=" isValid('CanAddSize') || isValid('CanEditSize') ">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">
                            <h5 class="modal-title DayHeading" id="myModalLabel">Cash Return</h5>
                        </div>
                        <div>
                            <div class="card-body ">
                                <div class="row ">
                                    <div class="form-group has-label col-sm-12 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label class="text  font-weight-bolder"> {{ $t('TemporaryCashReturn.Amount') }}:<span class="text-danger"> *</span></label>
                                        <input class="form-control" @keyup="CheckAmount($event.target.value)" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="temporaryCashReturn.amount" type="number" />
                                    </div>
                                    
                                    <div class="form-group has-label col-sm-12 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label class="text  font-weight-bolder"> {{ $t('TemporaryCashReturn.Date') }}:<span class="text-danger"> *</span></label>
                                        <datepicker v-model="temporaryCashReturn.date" />
                                    </div>

                                    <div class="form-group has-label col-sm-12 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label class="text  font-weight-bolder"> {{ $t('TemporaryCashReturn.Description') }}: </label>
                                        <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="temporaryCashReturn.description" type="text" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-if="!loading">
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveSize" v-bind:disabled="v$.temporaryCashReturn.$invalid || amount<temporaryCashReturn.amount"> {{ $t('TemporaryCashReturn.SaveAsPost') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('TemporaryCashReturn.Cancel') }}</button>
                            </div>
                        </div>
                        <div v-else>
                            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        props: ['show', 'temporaryCashIssueId', 'userId', 'isCashRequesterUser','amount'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                render: 0,
                arabic: '',
                english: '',
                loading: false,
                temporaryCashReturn: {
                    id: '00000000-0000-0000-0000-000000000000',
                    temporaryCashIssueId:'',
                    date:'',
                    amount:0,
                    description:'',
                    userId: '',
                    isCashRequesterUser: false,
                }
            }
        },
        validations() {
            return {
                temporaryCashReturn: {
                    temporaryCashIssueId: {
                        required
                    },
                    amount: {
                        required
                    },
                    date: {
                        required
                    },
                    userId: {
                        required
                    },
                    isCashRequesterUser: {
                        required
                    },
                }
                }
        },
        methods: {
            CheckAmount: function (value) {
                var root = this;
                if (value > this.amount) {
                    root.$swal({
                        title: "Warning!",
                        text: "Amount Exceed !",
                        type: 'warning',
                        icon: 'warning',
                        timer: 1500,
                        timerProgressBar: true,
                    });
                    this.temporaryCashReturn.amount = this.amount;
                }
            },

            close: function () {
                this.$emit('close');
            },

            SaveSize: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/EmployeeRegistration/AddTemporaryCashReturn', this.temporaryCashReturn, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.loading = false
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,
                            }).then(function () {
                                root.close();
                            });

                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
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
            this.temporaryCashReturn.date = moment().format("DD MMM YYYY");
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.temporaryCashReturn.temporaryCashIssueId = this.temporaryCashIssueId;
            this.temporaryCashReturn.userId = this.userId;
            this.temporaryCashReturn.isCashRequesterUser = this.isCashRequesterUser;
            this.temporaryCashReturn.amount = this.amount;
        }
    }
</script>
