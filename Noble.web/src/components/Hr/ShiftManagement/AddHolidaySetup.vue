<template>
    <modal show="show"  v-if=" isValid('CanAddColor') || isValid('CanEditColor') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">{{ $t('AddHoliday.UpdateHoliday') }} </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddHoliday.AddHoliday') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group col-sm-12">
                        <label>{{ $t('AddHoliday.HolidayType') }}</label>
                        <multiselect v-model="holiday.holidayType" :options="['National', 'Guested']" :show-labels="false" placeholder="Select Type">
                        </multiselect>
                    </div>

                    <div class="form-group col-sm-12">
                        <label>{{ $t('AddHoliday.Date') }}</label>
                        <datepicker v-model="holiday.date" />
                    </div>

                    <div class="form-group col-sm-12">
                        <label>Color</label>
                        <input v-model="holiday.color" type="color" class="form-control form-control-color border_input" id="exampleColorInput" title="Choose your color">
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('AddHoliday.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="holiday.description" type="text" />
                    </div>


                    <div class="form-group col-md-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="holiday.paidStatus">
                            <label for="inlineCheckbox1">{{ $t('AddHoliday.PaidStatus') }}  </label>
                        </div>
                    </div>

                    <div class="form-group col-md-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="holiday.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddHoliday.Status') }} </label>
                        </div>
                    </div>



                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor" v-bind:disabled="v$.holiday.$invalid" v-if="type!='Edit' && isValid('CanAddColor')">{{ $t('AddHoliday.Save') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor" v-bind:disabled="v$.holiday.$invalid" v-if="type=='Edit' && isValid('CanEditColor')">{{ $t('AddHoliday.Update') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddHoliday.Cancel') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
    export default {
        props: ['show', 'newHoliday', 'type'],
        mixins: [clickMixin],
        components: {
            Loading,
            Multiselect
        },
          setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                holiday: this.newHoliday,
                arabic: '',
                english: '',
                render: 0,
                loading: false,
            }
        },
        validations() {
           return{
             holiday: {
                holidayType: {
                    required
                },
                date: {
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
                this.$https.get('/Product/ColorCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.color.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveColor: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.post('/Hr/SaveHolidays', this.holiday, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.id != '00000000-0000-0000-0000-000000000000') {

                            if (root.type != "Edit") {

                                root.$swal({
                                    title: root.$t('AddColors.SavedSuccessfully'),
                                    text: root.$t('AddColors.Saved'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });

                                root.close();
                            }
                            else {

                                root.$swal({
                                    title: root.$t('AddColors.SavedSuccessfully'),
                                    text: root.$t('AddColors.UpdateSucessfully'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();
                            }
                        }
                        else {
                            root.$swal({
                                title: root.$t('AddColors.Error'),
                                text: root.$t('AddColors.YourColorNameAlreadyExist'),
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: root.$t('AddColors.SomethingWrong'),
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            //if (this.color.id == '00000000-0000-0000-0000-000000000000' || this.color.id == undefined || this.color.id == '')
            //    this.GetAutoCodeGenerator();

        }
    }
</script>
