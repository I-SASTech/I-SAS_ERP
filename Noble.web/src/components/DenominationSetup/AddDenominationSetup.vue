<template>
    <modal :show="show" v-if=" isValid('CanAddDenomination') || isValid('CanEditDenomination') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">{{
                        $t('AddDenominationSetup.UpdateDenominationSetup')
                }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddDenominationSetup.AddDenominationSetup') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm-12 form-group"
                        v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <label>{{ $t('AddDenominationSetup.DenominationNumber') }} :<span class="text-danger">
                                *</span></label>
                        <div v-bind:class="{ 'has-danger': v$.denominationSetup.number.$error }">
                            <input class="form-control" type="number" v-model="v$.denominationSetup.number.$model"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                            <span v-if="v$.denominationSetup.number.$error" class="error text-danger">
                                <span v-if="!v$.denominationSetup.number.required">{{ $t('AddDenominationSetup.Name')
                                }}</span>
                                <span v-if="!v$.denominationSetup.number.maxLength">{{
                                        $t('AddDenominationSetup.NameLength')
                                }}</span>
                            </span>
                        </div>
                    </div>

                    
                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="denominationSetup.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddDenominationSetup.Active') }} </label>
                        </div>
                    </div>
                    

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDenominationSetup"
                    v-bind:disabled="v$.denominationSetup.$invalid"
                    v-if="type != 'Edit' && isValid('CanAddDenomination')">{{ $t('AddDenominationSetup.btnSave')
                    }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDenominationSetup"
                    v-bind:disabled="v$.denominationSetup.$invalid"
                    v-if="type == 'Edit' && isValid('CanEditDenomination')">{{ $t('AddDenominationSetup.btnUpdate')
                    }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{
                        $t('AddDenominationSetup.btnClear')
                }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import { maxLength, required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

export default ({
    mixins: [clickMixin],
    props: ['show', 'newDenominationSetup', 'type'],
    setup() {
            return { v$: useVuelidate() }
        },
  components: {
            Loading
        },
    data: function () {
        return {
              loading: false,
            denominationSetup: this.newDenominationSetup,
            render: 0,
            arabic: '',
            english: '',

        }
    },
    validations() {
       return{
         denominationSetup:
        {
            number:
            {
                required,
                maxLength: maxLength(50)
            },
        }
       }
    },
    methods: {

        close: function () {
            this.$emit('close');
        },
        SaveDenominationSetup: function () {
            var root = this;
             this.loading = true;
            var url = '/Product/SaveDenominationSetup';
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https
                .post(url, root.denominationSetup, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isSuccess == true) {


                        if (root.type != "Edit") {
                            
                            root.$swal({
                                text: root.$t('AddDenominationSetup.Saved'),
                                title: root.$t('AddDenominationSetup.SavedSuccessfully'),
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
                                title: root.$t('AddDenominationSetup.Updated'),
                                text: root.$t('AddDenominationSetup.UpdateSuccessfully'),
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
                        root.$swal({
                            title: root.$t('AddDenominationSetup.Error'),
                            text: root.$t('AddDenominationSetup.NameAlreadyExist'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                    }
                    root.loading = false
                });
        }
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        if (this.$route.query.data != undefined) {
            this.denominationSetup = this.$route.query.data;
        }
    }
})

</script>