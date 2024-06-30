<template>
    <modal :show="show">
        <modal show="show">
            <div class="modal-content">
                <div class="modal-header">
                        <h6 class="modal-title m-0" id="myModalLabel"> {{ $t('Developer Setting Login') }}</h6>
                       <button type="button" class="btn-close" v-on:click="close()"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder"> {{ $t('TheSupervisorLogin.User') }}:<span class="text-danger"> *</span> </label>
                            <input class="form-control" v-model="login.username" type="text" />
                        </div>
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder"> {{ $t('TheSupervisorLogin.Password') }}:<span class="text-danger"> *</span> </label>
                            <input class="form-control" v-model="login.password" type="password" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="CheckLogin()"> {{ $t('TheSupervisorLogin.Login1') }}</button>
                    <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('TheSupervisorLogin.Cancel') }}</button>
                </div>
            </div>
        </modal>


    </modal>
    
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],
        
        props: ['show'],
        data: function () {
            return {
                login: {
                    username: '',
                    password: '',
                },
                username: 'Developer',
                password: 'Developer',
            }
        },
        methods: {
           
            close: function (x) {
                if (x) {
                    this.$emit('close', true);

                }
                else {
                    this.$emit('close', false);
                }
            },
            
            CheckLogin:function()
            {
                if(this.login.username == this.username && this.login.password == this.password)
                {
                    this.$emit('login', true);
                }
                else
                {
                    this.$swal({
                        title: "Error!",
                        text: "Wrong Username and Password",
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
            }
        },
        mounted: function () {
            
        }
    }
</script>
