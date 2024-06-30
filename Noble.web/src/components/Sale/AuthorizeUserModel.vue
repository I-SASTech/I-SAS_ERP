<template>
    <modal :show="show">
        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">
                            <h5 class="modal-title DayHeading" id="myModalLabel"> Login</h5>
                        </div>
                        <div class="text-left">
                            <div class="card-body">
                                <div class="row ">
                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.authorize.userName.$error}">
                                        <label class="text  font-weight-bolder"> User Name: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.authorize.userName.$model" type="text" />
                                        <span v-if="v$.authorize.userName.$error" class="error">
                                            <span v-if="!v$.authorize.userName.required"> Required</span>
                                        </span>
                                    </div>
                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.authorize.password.$error}">
                                        <label class="text  font-weight-bolder"> Password:<span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.authorize.password.$model" type="password" />
                                        <span v-if="v$.authorize.password.$error" class="error">
                                            <span v-if="!v$.authorize.password.required"> Required</span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-right">
                            <button type="button" class="btn btn-primary  " v-on:click="verifyAuthor" v-bind:disabled="v$.authorize.$invalid"> {{ $t('AuthorizeUserModel.btnSave') }}</button>
                            <button type="button" class="btn btn-secondary  mr-3 " v-on:click="close()">{{ $t('AuthorizeUserModel.btnClear') }}</button>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </modal>
</template>
<style scoped>
    .swal2-modal .swal2-content {
        font-weight: 400 !important;
    }
</style>
<script>
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'authorize'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                render: 0,
                result:''
            }
        },
        validations() {
            return {
                authorize: {
                    userName: {
                        required
                    },
                    password: {
                        required
                    },
                }
                }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },

            verifyAuthor: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.post('/Sale/AuthorizeUser', this.authorize, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isLoginFail == false && (response.data.changePriceDuringSale == true || response.data.giveDiscountDuringSale == true)) {  
                        root.result = response.data;
                        root.result.column = root.authorize.column;
                        root.$emit("result", root.result);
                        root.$swal({
                            title: "True!",
                            text: "You Are Authorized",
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success'
                        });

                        root.close();

                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your are Un Authorize!",
                            type: 'success',
                            confirmButtonClass: "btn btn-danger",
                            buttonStyling: false,
                            icon: 'error'
                        });
                    }
                });
            }
        },
        mounted: function () {



        }
    }
</script>
