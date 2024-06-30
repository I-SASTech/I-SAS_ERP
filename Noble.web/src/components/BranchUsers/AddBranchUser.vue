
<template>
    <modal :show="show" >

        <div class="modal-content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">
                            <h6 class="modal-title m-0" id="myModalLabel">      {{ $t('AddBranchUsers.UpdateBranchUsers') }}</h6>
                            <button type="button" class="btn-close" v-on:click="close()"></button>
                        </div>
                        <div class="modal-header" v-else>
                            <h6 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddBranchUsers.AddBranchUsers') }}</h6>
                            <button type="button" class="btn-close" v-on:click="close()"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row ">
                              

                                <div class="form-group has-label col-sm-12 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranchUsers.User') }}:<span class="text-danger"> *</span></label>
                                    <usersDropdown v-model="newBranchUser.userId" :modelValue="newBranchUser.userId" :alluser="true" />
                                </div>
                                <div class="form-group has-label col-sm-12 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranchUsers.Branch') }}:<span class="text-danger"> *</span></label>
                                    <branch-dropdown v-model="newBranchUser.branchId" :modelValue="newBranchUser.branchId" :ismultiple="true" />
                                </div>


                            </div>
                        </div>

                        <div class="modal-footer " v-if="type=='Edit'">
                            <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveBranch" v-bind:disabled="v$.newBranchUser.$invalid"> {{ $t('AddBranches.Update') }}</button>
                            <button type="button" class="btn btn-danger btn-sm" v-on:click="close()">{{ $t('AddBranches.Cancel') }}</button>
                        </div>
                        <div class="modal-footer justify-content-right" v-if="type!='Edit'">
                            <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveBranch" v-bind:disabled="v$.newBranchUser.$invalid"> {{ $t('AddBranches.Save') }}</button>
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
    
    

    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        mixins: [clickMixin],
        components: {
            
        },
        props: ['show', 'terminal', 'type'],
             setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading: false,
                newBranchUser: {},
            }
        },
        validations() {
           return{
             newBranchUser: {
                userId: {
                    required
                },
                branchId: {
                    required
                },
            }
           }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            SaveBranch: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.loading = true;

                this.$https.post('/Branches/SaveBranchUser', this.newBranchUser, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
            this.newBranchUser = this.terminal;

        },
        mounted: function () {

        }
    }
</script>
