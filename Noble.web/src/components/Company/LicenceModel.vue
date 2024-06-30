<template>
    <modal :show="show">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="editType=='Edit'">

                            <h5 class="modal-title" id="myModalLabel">   {{ $t('Company.UpdateLicense') }}</h5>

                        </div>
                        <div class="modal-header" v-else>

                            <h5 class="modal-title" id="myModalLabel">  {{ $t('Company.AddLicense') }}</h5>

                        </div>
                        {{licence.companyName}}
                        <div class="text-left">
                            <div class="card-body">
                                <div class="row ">
                                    <div class="col-sm-12">
                                        <label>{{ $t('Company.LicenseType') }}:<span class="text-danger"> *</span></label>
                                        <div>

                                            <link rel="stylesheet" href="https://unpkg.com/vue-multiselect@2.1.0/dist/vue-multiselect.min.css">
                                            <multiselect v-model="v$.licence.companyType.$model"
                                                         :options="types" :show-labels="false"
                                                         @update:modelValue="setDate"
                                                         placeholder="Select Type">
                                            </multiselect>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>{{ $t('Company.FromDate') }}</label>
                                        <div>
                                            <datepicker v-model="v$.licence.fromDate.$model" :key="render" />

                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>{{ $t('Company.ToDate') }}</label>
                                        <div>
                                            <datepicker v-model="v$.licence.toDate.$model" :key="render"></datepicker>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 form-group">
                                        <label>{{ $t('Company.NoOfUsers') }}</label>
                                        <div>
                                            <input type="number" v-model="licence.numberOfUsers" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12 form-group">
                                        <label>{{ $t('Company.NoOfTransactions') }}</label>
                                        <div>
                                            <input type="number" v-model="licence.numberOfTransactions" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <div class="col-md-4 float-left">
                                            <label style="margin: 7px;">{{ $t('Company.Active') }}</label> <br />
                                            <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': licence.isActive, 'bootstrap-switch-off': !licence.isActive}" v-on:click="licence.isActive = !licence.isActive" style="width: 72px;">
                                                <div class="bootstrap-switch-container" style="width: 122px; margin-left: 0px;">
                                                    <span class="bootstrap-switch-handle-on bootstrap-switch-success" style="width: 50px;">
                                                        <i class="nc-icon nc-check-2"></i>
                                                    </span>
                                                    <span class="bootstrap-switch-label" style="width: 30px;">&nbsp;</span>
                                                    <span class="bootstrap-switch-handle-off bootstrap-switch-success" style="width: 50px;">
                                                        <i class="nc-icon nc-simple-remove"></i>
                                                    </span>
                                                    <input class="bootstrap-switch" type="checkbox" data-toggle="switch" checked="" data-on-label="<i class='nc-icon nc-check-2'></i>" data-off-label="<i class='nc-icon nc-simple-remove'></i>" data-on-color="success" data-off-color="success">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 float-right">
                                            <label style="margin: 7px;">{{ $t('Company.Block') }}</label> <br />
                                            <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': licence.isBlock, 'bootstrap-switch-off': !licence.isBlock}" v-on:click="licence.isBlock = !licence.isBlock" style="width: 72px;">
                                                <div class="bootstrap-switch-container" style="width: 122px; margin-left: 0px;">
                                                    <span class="bootstrap-switch-handle-on bootstrap-switch-success" style="width: 50px;">
                                                        <i class="nc-icon nc-check-2"></i>
                                                    </span>
                                                    <span class="bootstrap-switch-label" style="width: 30px;">&nbsp;</span>
                                                    <span class="bootstrap-switch-handle-off bootstrap-switch-success" style="width: 50px;">
                                                        <i class="nc-icon nc-simple-remove"></i>
                                                    </span>
                                                    <input class="bootstrap-switch" type="checkbox" data-toggle="switch" checked="" data-on-label="<i class='nc-icon nc-check-2'></i>" data-off-label="<i class='nc-icon nc-simple-remove'></i>" data-on-color="success" data-off-color="success">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-right" v-if="editType=='Edit'">

                            <button type="button" class="btn btn-primary  " v-on:click="SaveLicence" :disabled="v$.$invalid"> {{ $t('Company.Update') }}</button>
                            <button type="button" class="btn btn-secondary  mr-3 " v-on:click="close()">{{ $t('Company.Cancel') }}</button>

                        </div>

                        <div class="modal-footer justify-content-right" v-else>

                            <button type="button" class="btn btn-primary  " v-on:click="SaveLicence" :disabled="v$.$invalid"> {{ $t('Company.Save') }}</button>
                            <button type="button" class="btn btn-secondary  mr-3 " v-on:click="close()">{{ $t('Company.Cancel') }}</button>

                        </div>
                        <div class="card-footer col-md-3" v-if="loading">
                            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </modal>
</template>
<script>
    import moment from "moment";
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    // import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    

    export default {
        components: {
            Multiselect,
            Loading
        },
        props: ['show', 'type', 'companyName', 'companyId', 'companyLicenceId', 'companyLicenceList','isLocations'],
        setup() {
            return { v$: useVuelidate() }
        },
       data: function () {
            return {
                editType: this.type,
                render: 0,
                licence: {
                    id: '00000000-0000-0000-0000-000000000000',
                    fromDate: '',
                    toDate: '',
                    isActive: true,
                    isBlock: false,
                    companyId: '',
                    companyName: '',
                    numberOfUsers: 0,
                    numberOfTransactions: 60,
                    companyType: ''
                },
                types: ['Trail', 'Basic', 'Standard', 'Advanced'],
                loading: false,

            }
        },
        validations() {
           return{
             licence: {
                companyType: { required },
                toDate: { required },
                fromDate: { required },
                companyName: { required },
            }
           }
        },

        methods: {
            close: function (x) {
                if (this.isLocations) {
                    this.$emit('close', true);
                }
                else {
                    this.$emit('close', x);
                }
            },
            SaveLicence: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.post('/Company/AddLicence', this.licence,
                    { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                       
                        
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: " Saved!",
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                        root.loading = false;
                        root.close(response.data);
                        
                       
                    });
            },

            setDate: function () {
                if (this.licence.companyType == "Trail" || this.licence.companyType == "Basic" || this.licence.companyType == "Standard" || this.licence.companyType == "Advanced") {
                    this.licence.fromDate = moment().startOf('month').format('llll');
                    this.licence.toDate = moment().endOf('month').format('llll');
                    this.render++;
                }
               
            }
        },

        mounted: function () {
            this.licence.companyName = this.companyName;
            this.licence.companyId = this.companyId;


            if (this.companyLicenceList.length > 0) {
                var licence = this.companyLicenceList.find(x => x.id == this.companyLicenceId);
                this.editType = 'Edit'
                this.licence = {
                    id: licence.id,
                    fromDate: moment(licence.fromDate).format('llll'),
                    toDate: moment(licence.toDate).format('llll'),
                    isActive: licence.isActive,
                    isBlock: licence.isBlock,
                    companyId: licence.companyId,
                    companyName: this.companyName,
                    numberOfUsers: licence.numberOfUsers,
                    numberOfTransactions: licence.numberOfTransactions,
                    companyType: this.types[licence.companyType - 1]
                }
            } else {
                this.setDate();
            }
            this.render++;
        }
    }
</script>
