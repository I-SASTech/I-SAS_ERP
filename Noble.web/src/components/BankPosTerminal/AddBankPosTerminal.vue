<template>
    <modal  :show="show" v-if="isValid('CanAddPosTerminal') || isValid('CanEditPosTerminal') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">{{ $t('AddBankPosTerminal.UpdateBankPosTerminal') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddBankPosTerminal.AddBankPosTerminal') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group col-sm-12">
                        <label class="text  font-weight-bolder"> {{ $t('AddBankPosTerminal.TerminalId') }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-model="v$.newBankPosTerminal.terminalId.$model" type="text" />
                        <span v-if="v$.newBankPosTerminal.terminalId.$error" class="error">
                            <span v-if="!v$.newBankPosTerminal.terminalId.required">{{ $t('AddBankPosTerminal.NameRequired') }}</span>
                            <span v-if="!v$.newBankPosTerminal.terminalId.maxLength">{{ $t('AddBankPosTerminal.NameLength') }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">   {{$t('AddBankPosTerminal.BankAccount')}}:<span class="text-danger"> *</span> </label>
                        <accountdropdown v-model="newBankPosTerminal.bankId" :formName="bank"></accountdropdown>
                    </div>


                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="newBankPosTerminal.isActive" v-on:click="newBankPosTerminal.isActive = !newBankPosTerminal.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddBankPosTerminal.Active') }} </label>
                        </div>
                    </div>

                </div>
            </div>
            <div class="modal-footer" v-if="type=='Edit'">

                <button type="button" class="btn btn-soft-primary btn-sm  " v-if="isValid('CanEditPosTerminal') " v-on:click="SaveBankPosTerminal" v-bind:disabled="v$.newBankPosTerminal.$invalid"> {{ $t('AddBankPosTerminal.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm " v-on:click="close()">{{ $t('AddBankPosTerminal.btnClear') }}</button>

            </div>
            <div class="modal-footer " v-if="type!='Edit'">

                <button type="button" class="btn btn-soft-primary btn-sm  " v-if="isValid('CanAddPosTerminal')" v-on:click="SaveBankPosTerminal" v-bind:disabled="v$.newBankPosTerminal.$invalid"> {{ $t('AddBankPosTerminal.btnSave') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm " v-on:click="close()">{{ $t('AddBankPosTerminal.btnClear') }}</button>

            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'bankPosTerminal', 'type'],
        components: {
            Loading
        },
        mixins: [clickMixin],
             setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading: false,
                render: 0,
                bank: 'BankReceipt',
                newBankPosTerminal: {}
            }
        },
        validations() {
          return{
              newBankPosTerminal: {
                terminalId: {
                    required,
                    maxLength: maxLength(50)
                },
                bankId: {
                    required,
                },

            }
          }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },

            SaveBankPosTerminal: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Company/SaveBankPosTerminal', this.newBankPosTerminal, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {

                        if (root.type != "Edit") {

                            root.$swal({
                                title: root.$t('AddBankPosTerminal.Saved'),
                                text: root.$t('AddBankPosTerminal.SavedSuccessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                            root.$router.go();

                        }
                        else {

                            root.$swal({
                                title: root.$t('AddBankPosTerminal.Saved'),
                                text: root.$t('AddBankPosTerminal.UpdateSucessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                            root.$router.go();

                        }
                    }
                    else {
                        root.$swal({
                            title: root.$t('AddBankPosTerminal.Error'),
                            text: root.$t('AddBankPosTerminal.BankPosTerminalNameExist'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                })
            }
        },
        created: function () {
            this.newBankPosTerminal = this.bankPosTerminal;

        }
    }
</script>
