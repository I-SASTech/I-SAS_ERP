<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('forgetPassword.RecoverPassword') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12">
                        <label class="labelHeading" style="margin-top:15px">  Email</label>
                        <input id="email" v-model="v$.emailTo.$model" name="email" type="text" class="form-control" v-bind:placeholder="$t('forgetPassword.EnterYourEmail')">
                        <small v-if="customError != ''" class="text-danger ms-2">
                            {{customError}}
                        </small>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SendEmail" v-bind:disabled="v$.$invalid">{{ $t('forgetPassword.Submit') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('forgetPassword.ForgotClose') }}</button>
            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true" />
    </modal>
</template>

<script>
    import { email, required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    
    export default {
        
        setup() {
            return { v$: useVuelidate() }
        },
        props: ['show'],
        data: function () {
            return {
                render: 0,
                emailTo: '',
                customError: '',
                loading: false
            }
        },
        validations() {
            return {
                emailTo: { email, required }
                }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            SendEmail: function () {
                var root = this;
                root.loading = true;
                root.customError = ''
                var url = '/account/SendUserEmail?emailTo=' + this.emailTo + '&appLink=' + this.$ClientIP;
                this.$https.get(url).then(function (response) {
                    if (response.data != "Error") {
                        root.loading = false;
                        root.$swal({
                            title: "Success!",
                            text: "An Email has sent to your personal Email Address regarding Reset Link.!",
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 900,
                            timerProgressBar: true,
                        });
                        root.close();
                    }
                    if (response.data == "Error") {

                        root.customError = "Please Enter Correct Email";
                        root.loading = false;
                    }
                }).catch(error => {
                    console.log(error)
                    root.customError = "Something thing went wrong";
                    root.loading = false;
                })
            }
        }
    }
</script>