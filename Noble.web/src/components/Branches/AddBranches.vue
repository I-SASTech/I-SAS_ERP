
<template>
    <modal :show="show" >

        <div class="modal-content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">
                            <h6 class="modal-title m-0" id="myModalLabel">      {{ $t('AddBranches.UpdateBranch') }}</h6>
                            <button type="button" class="btn-close" v-on:click="close()"></button>
                        </div>
                        <div class="modal-header" v-else>
                            <h6 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddBranches.AddBranches') }}</h6>
                            <button type="button" class="btn-close" v-on:click="close()"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row ">

                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranches.Code') }}:<span class="text-danger"> *</span></label>
                                    <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.terminal.code.$model" type="text" />
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranches.BranchName') }}:<span class="text-danger"> *</span></label>
                                    <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="terminal.branchName" type="text" />
                                </div>
                               
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> Branch Type:<span class="text-danger"> *</span></label>
                                    <multiselect v-model="terminal.branchType" :options="['Normal Branch', 'Branch With Accounting', 'Branch as Franchise']" :show-labels="false" placeholder="Select Branch Type">
                                </multiselect>
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranches.ContactNo') }}:<span class="text-danger"> *</span></label>
                                    <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="terminal.contactNo" type="text" />
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranches.Address') }}:<span class="text-danger"> *</span></label>
                                    <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="terminal.address" type="text" />
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranches.City') }}:</label>
                                    <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="terminal.city" type="text" />
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranches.State') }}:</label>
                                    <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="terminal.state" type="text" />
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranches.PostalCode') }}:</label>
                                    <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="terminal.postalCode" type="text" />
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranches.Country') }}:</label>
                                    <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="terminal.country" type="text" />
                                </div>
                               
                                 <div class="form-group has-label col-sm-4 ">
                                    <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox5" v-model="terminal.isCentralized">

                                    <label for="inlineCheckbox5"> Centerlized</label>
                                    </div>
                                 </div>
                                 <div class="form-group has-label col-sm-4 ">
                                    <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox55" v-model="terminal.isOnline">

                                    <label for="inlineCheckbox55"> Online</label>
                                    </div>
                                 </div>
                                 <div class="form-group has-label col-sm-4 ">
                                    <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox59" v-model="terminal.isApproval">

                                    <label for="inlineCheckbox59"> Approval</label>
                                    </div>
                                 </div>

                                <!-- <div class="form-group has-label col-sm-6 ">
                                    <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox1" v-model="terminal.mainBranch">

                                    <label for="inlineCheckbox1"> Main Branch</label>
                                    </div>
                                 </div> -->



                                <div class="form-group has-label col-sm-6 ">
                                    <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox1" v-model="terminal.isActive">
                                    <label for="inlineCheckbox1"> {{ $t('AddColors.Active') }} </label>
                        </div>
                                </div>


                            </div>
                        </div>

                        <div class="modal-footer " v-if="type=='Edit'">
                            <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveBranch" v-bind:disabled="v$.terminal.$invalid"> {{ $t('AddBranches.Update') }}</button>
                            <button type="button" class="btn btn-danger btn-sm" v-on:click="close()">{{ $t('AddBranches.Cancel') }}</button>
                        </div>
                        <div class="modal-footer justify-content-right" v-if="type!='Edit'">
                            <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveBranch" v-bind:disabled="v$.terminal.$invalid"> {{ $t('AddBranches.Save') }}</button>
                            <button type="button" class="btn btn-soft-secondary btn-sm " v-on:click="close()">{{ $t('AddBranches.Cancel') }}</button>
                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </modal>
</template>
<script>

    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Loading,
            Multiselect,

        },
        props: ['show', 'newbranch', 'type'],
             setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading: false,
                terminal: {},
            }
        },
        validations() {
         return{
               terminal: {
                code: {
                    required
                },
                branchName: {
                    required
                },
                contactNo: {
                    required
                },
                address: {
                    required
                },
            }
         }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Branches/GetBranchAutoCode?businessId=' + this.terminal.businessId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.terminal.code = response.data;
                        root.render++;
                        root.renderCode++;
                        root.renderPrinter++;
                    }
                });
            },
            SaveBranch: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.loading = true;

                this.$https.post('/Branches/SaveBranch', this.terminal, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {
                        root.loading = false;
                        if (root.type != "Edit") {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: "Your Branch has been created!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your Branch has been updated!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                    }
                    else {
                        root.loading = false;
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your terminal Name  Already Exist!",
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                    }
                });
            },
        },
        created: function () {
            this.terminal = this.newbranch;
        },
        mounted: function () {           
                this.GetAutoCodeGenerator();
        }
    }
</script>
